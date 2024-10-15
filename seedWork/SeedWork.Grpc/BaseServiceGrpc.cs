using ProtoBuf.Grpc;
using SeedWork.Infrastructure.Helpers;
using Shared.Mediator.Generics;

namespace SeedWork.Grpc;

public abstract class BaseServiceGrpc
{
    private readonly MediatorHelper _mediatorHelper;

    protected BaseServiceGrpc(MediatorHelper mediatorHelper)
    {
        _mediatorHelper = mediatorHelper;
    }

    protected async Task<TResult> Run<TCommand, TCommandResult, TResult>(object args, CallContext callContext)
        where TCommand : ICommand<TCommandResult>
    {
        return await _mediatorHelper.CommandWithResultAndMap<TCommand, TCommandResult, TResult>(args, callContext.CancellationToken);
    }

    protected async Task<TCommandResult> Run<TCommand, TCommandResult>(object args, CallContext callContext)
        where TCommand : ICommand<TCommandResult>
    {
        return await _mediatorHelper.CommandWithResult<TCommand, TCommandResult>(args, callContext.CancellationToken);
    }
}
