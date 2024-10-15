using Employees.Grpc.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Shared.Grpc.CodeFirstClient;

namespace Employees.Sdk;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEmployeesClient(this IServiceCollection services, string endPoint)
    {
        services
            .AddSingleton<EmployeesClient>()
            .AddCustomeGrpcClient<IEmployeesServiceGrpc>(endPoint);

        return services;
    }

}
