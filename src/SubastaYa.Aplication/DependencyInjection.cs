using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SubastaYa.Aplication;

public static class DependencyInjection
{
    public static IServiceCollection AddAplication(this IServiceCollection services)
    {
        services.AddMediatR(x => x.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        services.AddAutoMapper(config => { }, Assembly.GetExecutingAssembly());
        return services;
    }
}
