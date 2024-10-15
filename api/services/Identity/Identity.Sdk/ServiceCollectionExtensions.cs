using Identity.App.Infrastructure.Services;
using Identity.Grpc.Contracts;
using Identity.Infrastructure.Services.JwtSecurityToken;
using Microsoft.Extensions.DependencyInjection;
using Shared.Grpc.CodeFirstClient;

namespace Identity.Sdk;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentityClient(this IServiceCollection services, string endPoint)
    {
        services
            .AddSingleton<IdentityClient>()
            .AddCustomeGrpcClient<IIdentityServiceGrpc>(endPoint)
            .AddSingleton<IJwtTokenClaimService, JwtTokenClaimService>();

        return services;
    }


}
