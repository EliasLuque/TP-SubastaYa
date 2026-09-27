using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SubastaYa.Infrastructure.Persistence.Context;

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

        return services;
    }
}
