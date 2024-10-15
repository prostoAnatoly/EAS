using MediatR;

namespace Shared.Infrastruct.Authorization;

public interface IAuthorizationBehavior<TIn, TOut> : IPipelineBehavior<TIn, TOut> where TIn : IRequest<TOut>
{

}