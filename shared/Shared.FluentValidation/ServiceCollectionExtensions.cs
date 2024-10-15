using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Shared.FluentValidation;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection RegisterValidators(this IServiceCollection services, Assembly validatorsAssembly)
    {
        services.AddValidatorsFromAssembly(validatorsAssembly, ServiceLifetime.Singleton, includeInternalTypes: true);

        return services;
    }

}