using Identity.Infrastructure.Options;
using Microsoft.Extensions.DependencyInjection;
using SeedWork.Infrastructure;
using Shared.Infrastructure.PipelineInfrastructure;
using System.Reflection;
using Shared.Automapper;

namespace Identity.Infrastructure;

/// <summary>
/// Методы расширения <see cref="IServiceCollection"/>
/// </summary>
public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, Action<IdentityInfrastructureOptions> setOptions)
    {
        services
            .AddStandardPipelineInfrastructure<IdentityPipelineInfrastructure, IdentityInfrastructureOptions>(setOptions)
            .AddInfrastructureCore()
            .AddMediatorHelper()
            .RegisterAutomapper(Assembly.GetExecutingAssembly());

        return services;
    }
}