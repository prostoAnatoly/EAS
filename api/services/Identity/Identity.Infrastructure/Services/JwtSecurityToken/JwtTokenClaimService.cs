using Identity.App.Infrastructure.Services;
using Shared.Rest.Common.Extensions;
using System.Security.Claims;

namespace Identity.Infrastructure.Services.JwtSecurityToken;

public class JwtTokenClaimService : IJwtTokenClaimService
{

    public string ClaimNameIpAddress { get; } = "ip";

    public string ClaimNameUserAgent { get; } = "userAgent";

    /// <inheritdoc />
    public Claim CreateClaimByIpAddress(string ipAddress)
    {
        return new Claim(ClaimNameIpAddress, ipAddress);
    }

    /// <inheritdoc />
    public Claim CreateClaimByUserAgent(string userAgent)
    {
        return new Claim(ClaimNameUserAgent, userAgent);
    }

    /// <inheritdoc />
    public string? GetIpAddress(ClaimsIdentity identity)
    {
        return identity?.GetClaimValue(ClaimNameIpAddress);
    }

    /// <inheritdoc />
    public string? GetUserAgent(ClaimsIdentity identity)
    {
        return identity.GetClaimValue(ClaimNameUserAgent);
    }
}