using Microsoft.Extensions.DependencyInjection;

namespace Shared.Infrastructure.PipelineInfrastructure;

public abstract class PipelineInfrastructureBase<TInfrastructureOptions>
    where TInfrastructureOptions : IInfrastructureOptions
{

    public TInfrastructureOptions Options { get; }

    public PipelineInfrastructureBase(TInfrastructureOptions options)
    {
        Options = options;
    }

    public IServiceCollection AddPipelineInfrastructure(IServiceCollection services)
    {
        AddOptions(services);
        AddDatabaseContexts(services);
        AddRepositories(services);
        AddHttpClients(services);
        AddGrpcClients(services);
        AddEventBus(services);
        AddServices(services);

        return services;
    }

    protected virtual void AddOptions(IServiceCollection services)
    {

    }

    protected virtual void AddDatabaseContexts(IServiceCollection services)
    {

    }

    protected virtual void AddRepositories(IServiceCollection services)
    {

    }

    protected virtual void AddHttpClients(IServiceCollection services)
    {

    }

    protected virtual void AddGrpcClients(IServiceCollection services)
    {

    }

    protected virtual void AddServices(IServiceCollection serviceDescriptors)
    {

    }


    protected virtual void AddEventBus(IServiceCollection services)
    {

    }
}