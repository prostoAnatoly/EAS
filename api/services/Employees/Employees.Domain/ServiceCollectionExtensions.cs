using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Shared.Domain;

namespace Employees.Domain;

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

