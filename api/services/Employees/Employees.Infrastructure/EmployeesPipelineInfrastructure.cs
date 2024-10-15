using Employees.App.Infrastructure.DbContexts;
using Employees.App.ReadModel.DbContexts;
using Employees.Infrastructure.Options;
using Employees.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using SeedWork.Infrastructure.IntegrationBuses;
using SeedWork.Persistence;
using Shared.Infrastructure.PipelineInfrastructure;

namespace Employees.Infrastructure;

public class EmployeesPipelineInfrastructure : PipelineInfrastructureBase<EmployeesInfrastructureOptions>
{
    public EmployeesPipelineInfrastructure() : base(new EmployeesInfrastructureOptions())
    {
    }

    protected override void AddDatabaseContexts(IServiceCollection services)
    {
        base.AddDatabaseContexts(services);

        services
            .AddDefaultDbContext<EmployeesDatabaseContext>(Options)
            .AddScoped<IEmployeesContext>(provider => provider.GetRequiredService<EmployeesDatabaseContext>());

        services
            .AddDefaultDbContext<ReadModelsDatabaseContext>(Options)
            .AddScoped<IReadModelsContext>(provider => provider.GetRequiredService<ReadModelsDatabaseContext>());
    }

    protected override void AddEventBus(IServiceCollection services)
    {
        services.AddIntegrationBusAsRabbitMq();
    }
}
