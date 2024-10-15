using Microsoft.Extensions.DependencyInjection;
using SeedWork.Infrastructure.Authorization;
using SeedWork.Infrastructure.Helpers;
using Shared.Grpc;
using Shared.Infrastruct.Authorization;
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
            .AddSingleton<IHttpStatusCodeDefiner, HttpStatusCodeDefiner>()
            .AddAuthorizationService();

        return services;
    }

    public static IServiceCollection AddMediatorHelper(this IServiceCollection services)
    {
        services.AddTransient<MediatorHelper>();

        return services;
    }

    private static IServiceCollection AddAuthorizationService(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddSingleton<IAuthorizationContextCreator, AuthorizationContextCreator>();
        services.AddScoped<IAuthorizationService, AuthorizationService>();

        return services;
    }
}