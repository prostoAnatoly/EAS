using MediatR;

namespace Shared.Mediator;

public interface IQuery<out TOut> : IRequest<TOut>
{

}