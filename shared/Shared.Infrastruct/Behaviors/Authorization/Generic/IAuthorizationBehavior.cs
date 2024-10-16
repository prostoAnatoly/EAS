using MediatR;
using Shared.Infrastruct.Authorization;

namespace Shared.Infrastruct.Behaviors.Authorization.Generic;

public interface IAuthorizationBehavior<TIn, TOut> : IAuthorizationBehavior, IPipelineBehavior<TIn, TOut> where TIn : IRequest<TOut>
{
}