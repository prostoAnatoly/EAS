using Microsoft.Extensions.DependencyInjection;
using Shared.FluentValidation;
using Shared.Automapper;
using Shared.Mediator;
using System.Reflection;

namespace Shared.App;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppCore(this IServiceCollection services, Assembly applicationAssembly)
    {
        services
            .RegisterMediator(applicationAssembly)
            .RegisterValidators(applicationAssembly)
            .RegisterAutomapper(applicationAssembly);

        return services;
    }

}
