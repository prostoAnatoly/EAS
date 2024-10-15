using Microsoft.Extensions.DependencyInjection;
using Organizations.App.Infrastructure.DbContexts;
using Organizations.Infrastructure.Options;
using Organizations.Infrastructure.Persistence;
using SeedWork.Infrastructure.IntegrationBuses;
using SeedWork.Persistence;
using Shared.Infrastructure.PipelineInfrastructure;

namespace Organizations.Infrastructure;

public class OrganizationsPipelineInfrastructure : PipelineInfrastructureBase<OrganizationsInfrastructureOptions>
{
    public OrganizationsPipelineInfrastructure() : base(new OrganizationsInfrastructureOptions())
    {
    }

    protected override void AddDatabaseContexts(IServiceCollection services)
    {
        base.AddDatabaseContexts(services);

        services
            .AddDefaultDbContext<OrganizationsDatabaseContext>(Options)
            .AddScoped<IOrganizationsContext>(provider => provider.GetRequiredService<OrganizationsDatabaseContext>());
    }

    protected override void AddEventBus(IServiceCollection services)
    {
        services.AddIntegrationBusAsRabbitMq();
    }
}
