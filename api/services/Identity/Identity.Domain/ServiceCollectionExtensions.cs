using Microsoft.Extensions.DependencyInjection;
using Shared.Domain;
using System.Reflection;

namespace Identity.Domain;

/// <summary>
/// Методы расширения <see cref="IServiceCollection"/>
/// </summary>
public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddDomainCore(Assembly.GetExecutingAssembly());

        return services;
    }
}