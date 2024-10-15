using FilesStorage.App.Infrastructure.Services;
using FilesStorage.Infrastructure.Options;
using FilesStorage.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using SeedWork.Infrastructure;
using Shared.Infrastructure.PipelineInfrastructure;
using System.Reflection;
using Shared.Automapper;

namespace FilesStorage.Infrastructure;

/// <summary>
/// Методы расширения <see cref="IServiceCollection"/>
/// </summary>
public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, Action<FilesStorageInfrastructureOptions> setOptions)
    {
        services
            .AddStandardPipelineInfrastructure<FilesStoragePipelineInfrastructure, FilesStorageInfrastructureOptions>(setOptions)
            .AddInfrastructureCore()
            .AddMediatorHelper()
            .AddServices()
            .RegisterAutomapper(Assembly.GetExecutingAssembly());

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IFilesStorageService, FilesStorageService>();

        return services;
    }
}