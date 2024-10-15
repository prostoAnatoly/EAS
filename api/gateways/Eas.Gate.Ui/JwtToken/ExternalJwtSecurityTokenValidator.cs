using Identity.App.Infrastructure.Services;
using Identity.Grpc.Contracts.Arguments;
using Identity.Sdk;
using Microsoft.Net.Http.Headers;
using Shared.Rest.Common;
using Shared.Rest.Common.JwtToken;
using System.Security.Claims;

namespace EasGateUi.JwtToken;

internal class ExternalJwtSecurityTokenValidator : IExternalJwtSecurityTokenValidator
{
    private readonly IdentityClient _identityClient;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IJwtTokenClaimService _jwtTokenClaimService;

    public ExternalJwtSecurityTokenValidator(IdentityClient identityClient,
        IHttpContextAccessor httpContextAccessor, IJwtTokenClaimService jwtTokenClaimService)
    {
        _identityClient = identityClient;
        _httpContextAccessor = httpContextAccessor;
        _jwtTokenClaimService = jwtTokenClaimService;
    }

    public void ValidateClaimsIdentityThrow(ClaimsIdentity? identity)
    {
        if (identity == null) { return; }

        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null) { return; }

        #region Идентификация устройства

        // Проверка IP-адреса
        var ip = httpContext.GetIpAddress();
        var claimIp = _jwtTokenClaimService.GetIpAddress(identity);
        if (claimIp != ip)
        {
            throw new InvalidTokenException("Не верный токен. Не верный IP-адрес");
        }

        // Проверка заголовка User-Agent
        var userAgent = httpContext.GetHeaderValue(HeaderNames.UserAgent);
        var claimUserAgent = _jwtTokenClaimService.GetUserAgent(identity);
        if (claimUserAgent != userAgent)
        {
            throw new InvalidTokenException("Не верный токен. Не верный USER_AGENT");
        }

        #endregion
    }

    public async Task<ValidationResult> ValidateToken(string securityToken)
    {
        var args = new ValidateTokenGrpcArgs(securityToken);

        var resp = await _identityClient.ValidateToken(args);

        return new ValidationResult(resp.IsValid, resp.IdentityId);
    }
}
