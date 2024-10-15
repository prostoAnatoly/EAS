using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Shared.Automapper;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection RegisterAutomapper(this IServiceCollection services, Assembly configsAssembly)
    {
        services.AddAutoMapper(configsAssembly);

        return services;
    }

    public static IServiceCollection AutoMapperDisableNull(this IServiceCollection services)
    {
        services
            .Configure<MapperConfigurationExpression>(options =>
            {
                options.AllowNullCollections = false;
                options.AllowNullDestinationValues = false;
            });

        return services;
    }
}