using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Shared.App;

namespace Organizations.App;

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
