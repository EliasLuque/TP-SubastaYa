using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SubastaYa.Aplication.Commons.Pipelines;
using System.Reflection;

namespace SubastaYa.Aplication;

public static class DependencyInjection
{
    public static IServiceCollection AddAplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(config => 
        {
            config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());

            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(assembly);
        services.AddAutoMapper(config => { }, assembly);
        
        return services;
    }
}
