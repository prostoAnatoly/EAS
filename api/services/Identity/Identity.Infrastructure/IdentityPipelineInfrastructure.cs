using Identity.App.Infrastructure.DbContexts;
using Identity.App.Infrastructure.Services;
using Identity.Infrastructure.Options;
using Identity.Infrastructure.Persistence;
using Identity.Infrastructure.Services.JwtSecurityToken;
using Microsoft.Extensions.DependencyInjection;
using Shared.Common.Extensions;
using SeedWork.Infrastructure.IntegrationBuses;
using SeedWork.Persistence;
using Shared.Infrastructure.PipelineInfrastructure;

namespace Identity.Infrastructure;

public class IdentityPipelineInfrastructure : PipelineInfrastructureBase<IdentityInfrastructureOptions>
{
    public IdentityPipelineInfrastructure() : base(new IdentityInfrastructureOptions())
    {
    }

    protected override void AddDatabaseContexts(IServiceCollection services)
    {
        base.AddDatabaseContexts(services);

        services
            .AddDefaultDbContext<IdentityDatabaseContext>(Options)
            .AddScoped<IIdentitiesContext>(provider => provider.GetRequiredService<IdentityDatabaseContext>())
            .AddScoped<IAccessTokensContext>(provider => provider.GetRequiredService<IdentityDatabaseContext>());
    }

    protected override void AddOptions(IServiceCollection services)
    {
        base.AddOptions(services);

        services.AddOptionsFromBindWithValidateOnStart<JwtSecurityTokenOptions, JwtSecurityTokenOptionsValidator>();
    }

    protected override void AddEventBus(IServiceCollection services)
    {
        services.AddIntegrationBusAsRabbitMq();
    }

    protected override void AddServices(IServiceCollection services)
    {
        base.AddServices(services);

        services.AddSingleton<IJwtSecurityTokenService, JwtSecurityTokenService>();
        services.AddSingleton<IJwtTokenClaimService, JwtTokenClaimService>();
    }
}
