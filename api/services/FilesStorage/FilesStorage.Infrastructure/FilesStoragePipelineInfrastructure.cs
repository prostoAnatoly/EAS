using FilesStorage.App.Infrastructure.DbContexts;
using FilesStorage.Infrastructure.Options;
using FilesStorage.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using SeedWork.Infrastructure.IntegrationBuses;
using SeedWork.Persistence;
using Shared.Infrastructure.PipelineInfrastructure;

namespace FilesStorage.Infrastructure;

public class FilesStoragePipelineInfrastructure : PipelineInfrastructureBase<FilesStorageInfrastructureOptions>
{
    public FilesStoragePipelineInfrastructure() : base(new FilesStorageInfrastructureOptions())
    {
    }

    protected override void AddDatabaseContexts(IServiceCollection services)
    {
        base.AddDatabaseContexts(services);

        services
            .AddDefaultDbContext<FilesStorageDatabaseContext>(Options)
            .AddScoped<IFilesStoragesContext>(provider => provider.GetRequiredService<FilesStorageDatabaseContext>());
    }

    protected override void AddEventBus(IServiceCollection services)
    {
        services.AddIntegrationBusAsRabbitMq();
    }
}