using Identity.Domain.Aggregates.AccessTokens;
using Identity.Domain.Aggregates.Identities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Identity.App.Infrastructure.Services;

public interface IJwtSecurityTokenService
{

    AccessToken CreateToken(IdentityId identityId, string ipAddress, string userAgent, IEnumerable<Claim>? claims = null);

    JwtSecurityToken GetJwtSecurityToken(string accessToken);
}
