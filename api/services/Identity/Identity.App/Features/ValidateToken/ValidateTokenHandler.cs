using Identity.App.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Shared.Mediator;

namespace Identity.App.Features.ValidateToken;

sealed class ValidateTokenHandlerHandler : IQueryHandler<ValidateTokenQuery, ValidateTokenResult>
{
    private readonly IAccessTokensContext accessTokenContext;

    public ValidateTokenHandlerHandler(IAccessTokensContext accessTokenContext)
    {
        this.accessTokenContext = accessTokenContext;
    }

    public async Task<ValidateTokenResult> Handle(ValidateTokenQuery request, CancellationToken cancellationToken)
    {
        var accessToken = await accessTokenContext.AccessTokens
            .Select(x => new
        {
            x.IdentityId,
            x.Value,
        }).FirstOrDefaultAsync(x => x.Value == request.AccessToken, cancellationToken);

        if (accessToken == null)
        {
            return ValidateTokenResult.Empty;
        }

        return new ValidateTokenResult
        {
            IsValid = true,
            IdentityId = accessToken.IdentityId,
        };
    }

}