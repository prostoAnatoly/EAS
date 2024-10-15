using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shared.Infrastructure.Behaviors.IntegrationEvents;
using System.Reflection;
using Shared.Common.Extensions;

namespace SeedWork.Infrastructure.IntegrationBuses;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIntegrationBusAsRabbitMq(this IServiceCollection services)
    {
        services.AddOptionsFromBindWithValidateOnStart<IntegrationBusOptions, IntegrationBusOptionsValidator>();

        var options = services
            .BuildServiceProvider()
            .GetRequiredService<IOptions<IntegrationBusOptions>>()
            .Value;

        if (!options.Enabled)
        {
            services.AddScoped<IIntegrationBus, IntegrationBusStub>();
            
            return services;
        }

        services.AddScoped<IIntegrationBus, IntegrationBus>();
        services.AddMassTransit(builder =>
        {
            builder.AddConsumers(Assembly.GetEntryAssembly());

            builder.UsingRabbitMq(ConfigureBus);
        });

        return services;

        void ConfigureBus(IBusRegistrationContext context, IRabbitMqBusFactoryConfigurator cfg)
        {
            cfg.Host(options.Host, options.VirtualHost,
                opts =>
                {
                    opts.Username(options.Username);
                    opts.Password(options.Password);
                });

            cfg.ReceiveEndpoint(options.ReceiveEndpoint, ep =>
            {
                ep.PrefetchCount = 8;
                ep.UseMessageRetry(r =>
                {
                    SetInterval(r, options.RetryCount, options.RetryInitialIntervalMs,
                        options.RetryIntervalIncrementMs);
                });
            });

            cfg.ConfigureEndpoints(context);
        }
    }

    static void SetInterval(IRetryConfigurator configurator, int retryCount, int initialIntervalMs,
        int intervalIncrememtMs)
    {
        if (retryCount <= 0)
        {
            configurator.None();
        }
        else
        {
            configurator.Incremental(retryCount, TimeSpan.FromMilliseconds(initialIntervalMs),
                TimeSpan.FromMilliseconds(intervalIncrememtMs));
        }
    }
}
