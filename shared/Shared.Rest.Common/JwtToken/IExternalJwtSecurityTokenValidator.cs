using System.Security.Claims;

namespace Shared.Rest.Common.JwtToken;

public interface IExternalJwtSecurityTokenValidator
{
    Task<ValidationResult> ValidateToken(string securityToken);

    void ValidateClaimsIdentityThrow(ClaimsIdentity? identity);
}
