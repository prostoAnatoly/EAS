using Eas.Gate.Ui;
using Eas.Gate.Ui.Models.Invitations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Shared.Rest.Common.Controllers;
using System.ComponentModel.DataAnnotations;

namespace EasGateUi.Controllers;

/// <summary>
/// API по работе с приглашениями
/// </summary>
[Route("api/invitations")]
[ApiVersion("1.0")]
[EnableCors(CorsPolicyName.MAIN_POLICY)]
public class InvitationsController : ApiControllerBase
{

    [HttpPost("check/{code}")]
    [AllowAnonymous]
    [EnableCors(CorsPolicyName.ALLOW_ANONYMOUS_POLICY)]
    public async Task<IActionResult> Сheck([FromRoute][Required] string code)
    {
        if (code != "12345")
        {
            return GetBadRequest("Код приглашения не действительный");
        }

        var result = new CheckResponse
        {
            UserName = "email@test.com",
        };

        return GetOk(result);
    }

}