using Microsoft.Extensions.DependencyInjection;
using Organizations.Grpc.Contracts;
using Shared.Grpc.CodeFirstClient;

namespace Organizations.Sdk;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOrganizationsClient(this IServiceCollection services, string endPoint)
    {
        services
            .AddSingleton<OrganizationsClient>()
            .AddCustomeGrpcClient<IOrganizationsServiceGrpc>(endPoint);

        return services;
    }

}
