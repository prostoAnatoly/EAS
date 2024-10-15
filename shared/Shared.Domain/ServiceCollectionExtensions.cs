using Microsoft.Extensions.DependencyInjection;
using Shared.Domain.Services;
using System.Reflection;

namespace Shared.Domain;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainCore(this IServiceCollection services, Assembly domainAssembly)
    {
        services
            .AddDomainServices(domainAssembly);

        return services;
    }

    private static IServiceCollection AddDomainServices(this IServiceCollection services, Assembly domainServices)
    {
        services.Scan(scan =>
        {
            scan.FromAssemblies(domainServices)
                .AddClasses(classes => classes.AssignableTo<IDomainService>())
                .AsImplementedInterfaces()
                .WithScopedLifetime();
        });

        return services;
    }

}
