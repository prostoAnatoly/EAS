using Eas.Gate.Ui;
using Eas.Gate.Ui.Models.Identity;
using EasGateUi.Models.Identity;
using Identity.Grpc.Contracts.Arguments;
using Identity.Sdk;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Shared.Rest.Common;
using Shared.Rest.Common.Controllers;

namespace EasGateUi.Controllers;

/// <summary>
/// API по работе с идентификацией пользователя
/// </summary>
[Route("api/auth")]
[ApiVersion("1.0")]
[EnableCors(CorsPolicyName.MAIN_POLICY)]
public class IdentityController : ApiControllerBase
{
    private readonly IdentityClient identityClient;

    public IdentityController(IdentityClient identityClient)
    {
        this.identityClient = identityClient;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    [EnableCors(CorsPolicyName.ALLOW_ANONYMOUS_POLICY)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var ipAddress = Request.GetIpAddress();
        var userAgent = Request.GetHeaderValue(HeaderNames.UserAgent);

        var args = new LoginGrpcArgs(request.UserName, request.Password, ipAddress, userAgent);
        var loginResult = await identityClient.Login(args);

        var result = new AuthBaseInfo(loginResult.AccessToken, loginResult.ExpiresIn, loginResult.RefreshToken);

        return GetOk(result);
    }

    [HttpGet("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        var accessToken = this.GetAccessTokenFromHeader();
        var args = new LogoutGrpcArgs(accessToken);

        await identityClient.Logout(args);

        var modelOut = new LogoutResponse(true);

        return GetOk(modelOut);
    }
}