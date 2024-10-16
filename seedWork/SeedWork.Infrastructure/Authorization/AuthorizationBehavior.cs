using MediatR;
using SeedWork.Application.Authorization;
using Shared.Infrastruct.Behaviors.Authorization.Generic;

namespace SeedWork.Infrastructure.Authorization;

sealed class AuthorizationBehavior<TIn, TOut> : IAuthorizationBehavior<TIn, TOut> where TIn : IRequest<TOut>
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IAuthorizer<TIn>? _authorizer;

    public AuthorizationBehavior(IAuthorizationService authorizationService, IAuthorizer<TIn>? authorizer)
    {
        _authorizationService = authorizationService;
        _authorizer = authorizer;
    }

    public async Task<TOut> Handle(TIn request, RequestHandlerDelegate<TOut> next, CancellationToken cancellationToken)
    {
        if (_authorizer != null)
        {
            var authorizationContext = _authorizationService.GetAuthorizationContext()
                ?? throw new UnauthorizedAccessException("Запрос не авторизован");

            await _authorizer.Authorize(request, authorizationContext, cancellationToken);
        }

        return await next();
    }

}
