using Identity.App.Infrastructure.Services;
using Identity.Domain.Aggregates.AccessTokens;
using Identity.Domain.Aggregates.Identities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.Common.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using JwtToken = System.IdentityModel.Tokens.Jwt.JwtSecurityToken;

namespace Identity.Infrastructure.Services.JwtSecurityToken;

class JwtSecurityTokenService : IJwtSecurityTokenService
{
    private readonly static string claimNameIat = "iat";

    private readonly JwtSecurityTokenOptions _options;

    public JwtSecurityTokenService(IOptions<JwtSecurityTokenOptions> options)
    {
        _options = options.Value;
    }

    public AccessToken CreateToken(IdentityId identityId, string ipAddress, string userAgent, IEnumerable<Claim>? claims = null)
    {
        var now = DateTime.UtcNow;
        var defClaims = new Claim[]
            {
                new(claimNameIat, DateTimeUtils.ConvertToUnixTime(now).ToString(), ClaimValueTypes.Integer32),
            };

        claims ??= Enumerable.Empty<Claim>();
        claims = defClaims.Union(claims);

        var signingCredentials = new SigningCredentials(_options.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256);
        var expires = now.AddSeconds(_options.Lifetime);

        var jwt = new JwtToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            notBefore: now,
            claims: claims,
            expires: expires,
            signingCredentials: signingCredentials);

        var accessTokenValue = new JwtSecurityTokenHandler().WriteToken(jwt);
        var refreshToken = CreateRefreshToken(ipAddress, userAgent);

        return new AccessToken
        {
            Value = accessTokenValue,
            Expires = expires,
            ExpiresIn = _options.Lifetime,
            IdentityId = identityId,
            RefreshToken = refreshToken
        };
    }

    private RefreshToken CreateRefreshToken(string ipAddress, string userAgent)
    {
        var fingerprint = $"UserAgent = {userAgent}";
        var refreshTokenValue = GenerateRefreshTokenValue();
        var createdAt = DateTimeOffset.UtcNow;
        var expires = createdAt.AddDays(_options.RefreshTokenExpiration);

        return new RefreshToken
        {
            Value = refreshTokenValue,
            Expires = expires,
            CreatedAt = createdAt,
            CreatedByIp = ipAddress,
            Fingerprint = fingerprint,
        };
    }

    private static string GenerateRefreshTokenValue()
    {
        var randomBytes = RandomNumberGenerator.GetBytes(64);

        return Convert.ToBase64String(randomBytes);
    }

    public JwtToken GetJwtSecurityToken(string accessToken)
    {
        return new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
    }
}