using FilesStorage.Grpc.Cotracts;
using Microsoft.Extensions.DependencyInjection;
using Shared.Grpc.CodeFirstClient;

namespace FilesStorage.Sdk;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFilesStorageClient(this IServiceCollection services, string endPoint)
    {
        services
            .AddSingleton<FilesStorageClient>()
            .AddCustomeGrpcClient<IFilesStorageServiceGrpc>(endPoint);

        return services;
    }

}
