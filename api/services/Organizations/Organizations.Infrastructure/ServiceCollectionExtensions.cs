using Microsoft.Extensions.DependencyInjection;
using Organizations.Infrastructure.Options;
using Shared.Infrastructure.PipelineInfrastructure;
using SeedWork.Infrastructure;
using Shared.Automapper;
using System.Reflection;
using Organizations.App.Infrastructure.Repositories;
using Organizations.Infrastructure.Repositories;

namespace Organizations.Infrastructure;

/// <summary>
/// Методы расширения <see cref="IServiceCollection"/>
/// </summary>
public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        Action<OrganizationsInfrastructureOptions> setOptions)
    {
        services
            .AddStandardPipelineInfrastructure<OrganizationsPipelineInfrastructure, OrganizationsInfrastructureOptions>(setOptions)
            .AddInfrastructureCore()
            .AddMediatorHelper()
            .RegisterAutomapper(Assembly.GetExecutingAssembly())
            .AddRepositories();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IOrganizationsRepository, OrganizationsRepository>();

        return services;
    }
}