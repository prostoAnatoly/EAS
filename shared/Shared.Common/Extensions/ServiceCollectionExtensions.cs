using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Shared.Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static OptionsBuilder<TOptions> AddOptionsFromBindWithValidateOnStart<TOptions, TValidator>(
        this IServiceCollection services, string? configSectionPath = null)
        where TOptions : class
        where TValidator : class, IValidateOptions<TOptions>
    {
        services.AddSingleton<IValidateOptions<TOptions>, TValidator>();

        return services
            .AddOptions<TOptions>()
            .BindConfiguration(configSectionPath: configSectionPath ?? typeof(TOptions).Name)
            .ValidateOnStart();
    }
}
