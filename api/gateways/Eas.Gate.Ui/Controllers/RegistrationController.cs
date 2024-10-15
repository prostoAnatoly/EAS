using Eas.Gate.Ui;
using Eas.Gate.Ui.Models.Registration;
using Identity.Grpc.Contracts.Arguments;
using Identity.Sdk;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Shared.Rest.Common.Controllers;
using System.ComponentModel.DataAnnotations;

namespace EasGateUi.Controllers;

/// <summary>
/// API по регистрации
/// </summary>
[Route("api/registration")]
[ApiVersion("1.0")]
[EnableCors(CorsPolicyName.MAIN_POLICY)]
public class RegistrationController(IdentityClient identityClient) : ApiControllerBase
{
    [HttpPost("user-by-code/{invitationCode}")]
    [AllowAnonymous]
    [EnableCors(CorsPolicyName.ALLOW_ANONYMOUS_POLICY)]
    public async Task<IActionResult> RegistrationUserByCode([FromRoute][Required] string invitationCode,
        [Required][FromBody] UserModel employee)
    {
        if (invitationCode != "12345")
        {
            return GetBadRequest(" од приглашени€ не действительный");
        }

        var args = new RegistrationGrpcArgs(employee.UserName, employee.Password);
        var identityId = await identityClient.Registration(args);

        var result = new RegistrationUserByCodeResponse();

        return GetOk(result);
    }

    [HttpPost("user")]
    [AllowAnonymous]
    [EnableCors(CorsPolicyName.ALLOW_ANONYMOUS_POLICY)]
    public async Task<IActionResult> RegistrationUser([Required][FromBody] UserModel employee)
    {
        var args = new RegistrationGrpcArgs(employee.UserName, employee.Password);
        var identityId = await identityClient.Registration(args);

        var result = new RegistrationUserResponse();

        return GetOk(result);
    }
}