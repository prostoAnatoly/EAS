using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.Behaviors;
namespace Shared.Infrastructure.PipelineInfrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddStandardPipelineInfrastructure<TPipelineInfrastructure, TInfrastructureOptions>(
        this IServiceCollection services, Action<TInfrastructureOptions> setOptions)
        where TPipelineInfrastructure : PipelineInfrastructureBase<TInfrastructureOptions>, new()
        where TInfrastructureOptions : IInfrastructureOptions
    {
        var pipeline = new TPipelineInfrastructure();
        setOptions(pipeline.Options);
        pipeline.Options.Validate();

        services
            .AddStandardPipelineBehaviors();

        pipeline.AddPipelineInfrastructure(services);

        return services;
    }

}
