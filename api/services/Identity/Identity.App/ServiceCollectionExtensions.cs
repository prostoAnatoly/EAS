using Microsoft.Extensions.DependencyInjection;
using Shared.App;
using System.Reflection;

namespace Identity.App;

/// <summary>
/// Методы расширения <see cref="IServiceCollection"/>
/// </summary>
public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAppCore(Assembly.GetExecutingAssembly());

        return services;
    }
}