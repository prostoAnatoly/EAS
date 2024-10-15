using MediatR;

namespace Shared.Mediator;

public abstract class BaseCommandHandler<TCommand> : IRequestHandler<TCommand> where TCommand : ICommand
{
    async Task IRequestHandler<TCommand>.Handle(TCommand request, CancellationToken cancellationToken)
    {
        await Handle(request, cancellationToken);
    }

    protected abstract Task Handle(TCommand request, CancellationToken cancellationToken);
}