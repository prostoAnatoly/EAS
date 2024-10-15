using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Shared.Mediator;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection RegisterMediator(this IServiceCollection services, Assembly handlersAssembly)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(handlersAssembly));

        return services;
    }

}