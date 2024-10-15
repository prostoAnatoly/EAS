using Employees.Domain.Aggregates.Employees;
using Employees.Infrastructure.Options;
using Employees.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using SeedWork.Infrastructure;
using Shared.Automapper;
using Shared.Infrastructure.PipelineInfrastructure;
using System.Reflection;

namespace Employees.Infrastructure;

/// <summary>
/// Методы расширения <see cref="IServiceCollection"/>
/// </summary>
public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        Action<EmployeesInfrastructureOptions> setOptions)
    {
        services
            .AddStandardPipelineInfrastructure<EmployeesPipelineInfrastructure, EmployeesInfrastructureOptions>(setOptions)
            .AddInfrastructureCore()
            .AddMediatorHelper()
            .RegisterAutomapper(Assembly.GetExecutingAssembly())
            .AddRepositories();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IEmployeesRepository, EmployeesRepository>();

        return services;
    }
}