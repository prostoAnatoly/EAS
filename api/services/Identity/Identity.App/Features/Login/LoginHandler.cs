using Identity.App.Infrastructure.DbContexts;
using Identity.App.Infrastructure.Services;
using Identity.Domain.Aggregates.Identities;
using Microsoft.EntityFrameworkCore;
using Shared.App.Exceptions;
using Shared.Mediator;
using System.Security.Claims;

namespace Identity.App.Features.Login;

sealed class LoginHandler : ICommandHandler<LoginCommand, LoginResult>
{
    private readonly IIdentitiesContext identityContext;
    private readonly IJwtSecurityTokenService jwtSecurityTokenService;
    private readonly IJwtTokenClaimService jwtTokenClaimService;
    private readonly IAccessTokensContext accessTokenContext;

    public LoginHandler(IIdentitiesContext identityContext, IJwtSecurityTokenService jwtSecurityTokenService,
        IJwtTokenClaimService jwtTokenClaimService, IAccessTokensContext accessTokenContext)
    {
        this.identityContext = identityContext;
        this.jwtSecurityTokenService = jwtSecurityTokenService;
        this.jwtTokenClaimService = jwtTokenClaimService;
        this.accessTokenContext = accessTokenContext;
    }

    public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var identity = await identityContext.Identities
            .Where(u => u.UserName == request.UserName && u.Password == request.Password)
            .FirstOrDefaultAsync(cancellationToken)
                ?? throw new BadRequestException("Неверно имя или пароль");
        
        var claims = GetClaimsByUserAccount(request, identity);

        var accessToken = jwtSecurityTokenService.CreateToken(identity.Id, request.IpAddress, request.UserAgent, claims);

        accessTokenContext.AccessTokens.Add(accessToken);

        return new LoginResult(accessToken.Value, accessToken.ExpiresIn, accessToken.RefreshToken.Value);
    }

    private Claim[] GetClaimsByUserAccount(LoginCommand request, IdentityInfo identity)
    {
        return
            [
                jwtTokenClaimService.CreateClaimByIpAddress(request.IpAddress),
                jwtTokenClaimService.CreateClaimByUserAgent(request.UserAgent),
            ];
    }
}
