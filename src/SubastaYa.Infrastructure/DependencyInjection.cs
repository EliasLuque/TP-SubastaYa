using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SubastaYa.Aplication.Interface.Authentication;
using SubastaYa.Aplication.Interface.Persistence;
using SubastaYa.Aplication.Interface.Services;
using SubastaYa.Infrastructure.Authentication;
using SubastaYa.Infrastructure.Persistence.Context;
using SubastaYa.Infrastructure.Persistence.Repositories;
using SubastaYa.Infrastructure.Persistence.Services;

namespace SubastaYa.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        var assembly = typeof(SubastaYaDbContext).Assembly.FullName;

        services.AddDbContext<SubastaYaDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("SubastaYaConnection"), sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(assembly);
            });
        });

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
}
