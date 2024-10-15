using Microsoft.Extensions.DependencyInjection;
using SeedWork.Infrastructure.Helpers;
using Shared.Grpc;
using Shared.Rest.Common;

namespace SeedWork.Infrastructure;

/// <summary>
/// Методы расширения <see cref="IServiceCollection"/>
/// </summary>
public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddInfrastructureCore(this IServiceCollection services)
    {
        services
            .AddSingleton<IGrpcStatusCodeDefiner, GrpcStatusCodeDefiner>()
            .AddSingleton<IHttpStatusCodeDefiner, HttpStatusCodeDefiner>();

        return services;
    }

    public static IServiceCollection AddMediatorHelper(this IServiceCollection services)
    {
        services.AddTransient<MediatorHelper>();

        return services;
    }
}