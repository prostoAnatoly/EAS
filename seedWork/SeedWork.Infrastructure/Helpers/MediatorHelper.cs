using AutoMapper;
using MediatR;
using Shared.Mediator;
using Shared.Mediator.Generics;

namespace SeedWork.Infrastructure.Helpers;

public class MediatorHelper
{
    private readonly IMediator mediator;
    private readonly IMapper mapper;

    public MediatorHelper(IMediator mediator, IMapper mapper)
    {
        this.mediator = mediator;
        this.mapper = mapper;
    }

    public async Task<TQueryResult> Query<TQuery, TQueryResult>(object args, CancellationToken cancellationToken = default)
        where TQuery : IQuery<TQueryResult>
    {
        var query = mapper.Map<TQuery>(args);

        return await mediator.Send(query, cancellationToken);
    }

    public async Task<TResult> Query<TQuery, TQueryResult, TResult>(object args, CancellationToken cancellationToken = default)
        where TQuery : IQuery<TQueryResult>
    {
        var query = mapper.Map<TQuery>(args);

        var result = await mediator.Send(query, cancellationToken);

        return mapper.Map<TResult>(result);
    }

    public async Task<(TQueryResult, TResult)> QueryWithIntermediate<TQuery, TQueryResult, TResult>(object args,
        CancellationToken cancellationToken = default)
        where TQuery : IQuery<TQueryResult>
    {
        var query = mapper.Map<TQuery>(args);

        var queryResult = await mediator.Send(query, cancellationToken);
        var result = mapper.Map<TResult>(queryResult);

        return (queryResult, result);
    }

    public async Task Command<TCommand>(object args, CancellationToken cancellationToken = default)
        where TCommand: ICommand
    {
        var command = mapper.Map<TCommand>(args);

        await mediator.Send(command!, cancellationToken);
    }

    public async Task<TResult> CommandWithResultAndMap<TCommand, TCommandResult, TResult>(object args,
        CancellationToken cancellationToken = default)
        where TCommand : ICommand<TCommandResult>
    {
        var result = await CommandWithResult<TCommand, TCommandResult>(args, cancellationToken);

        return mapper.Map<TResult>(result);
    }

    public async Task<TCommandResult> CommandWithResult<TCommand, TCommandResult>(object args,
        CancellationToken cancellationToken = default)
        where TCommand : ICommand<TCommandResult>
    {
        var command = mapper.Map<TCommand>(args);

        return await mediator.Send(command!, cancellationToken);
    }
}
