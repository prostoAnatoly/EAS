using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shared.App;
using Shared.Infrastruct.Authorization;
using Shared.Infrastructure.Behaviors.DomainEvents;
using Shared.Infrastructure.Behaviors.IntegrationEvents;
using Shared.Infrastructure.Behaviors.Logging;
using Shared.Infrastructure.Behaviors.UnitOfWork;
using Shared.Infrastructure.Behaviors.Validation;

namespace Shared.Infrastructure.Behaviors;

public static class BehaviorsExtensions
{

    public static IServiceCollection AddStandardPipelineBehaviors(this IServiceCollection services)
    {
        services
            .AddLoggingBehavior()
            .AddValidationBehavior()
            .AddIntegrationEventsDispatchingBehavior()
            .AddUnitOfWorkBehavior()
            .AddDomainEventsDispatchingBehavior()
            .AddErrorHandler();

        return services;
    }
  
    public static IServiceCollection AddLoggingBehavior(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

        return services;
    }

    public static IServiceCollection AddValidationBehavior(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }

    public static IServiceCollection AddIntegrationEventsDispatchingBehavior(this IServiceCollection services)
    {
        services
            .AddScoped<IIntegrationEventsPublisher, IntegrationEventsPublisher>()
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(IntegrationEventsDispatchingBehavior<,>));

        return services;
    }

    public static IServiceCollection AddUnitOfWorkBehavior(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));

        return services;
    }

    public static IServiceCollection AddDomainEventsDispatchingBehavior(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(DomainEventsDispatchingBehavior<,>));

        return services;
    }

    public static IServiceCollection AddErrorHandler(this IServiceCollection services)
    {

        return services;
    }
}
