using MediatR;

namespace Shared.Mediator.Generics;

public interface ICommand<out TOut> : IRequest<TOut>
{

}