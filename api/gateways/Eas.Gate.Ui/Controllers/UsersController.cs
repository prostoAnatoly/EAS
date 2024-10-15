using Eas.Gate.Ui;
using Eas.Gate.Ui.Models.Common;
using Eas.Gate.Ui.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Shared.Rest.Common;
using Shared.Rest.Common.Controllers;

namespace EasGateUi.Controllers;

/// <summary>
/// API по работе с пользователями
/// </summary>
[Route("api/users")]
[ApiVersion("1.0")]
[EnableCors(CorsPolicyName.MAIN_POLICY)]
public class UsersController : ApiControllerBase
{

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> Me()
    {
        var accessToken = this.GetAccessTokenFromHeader();

        // TODO: реализовать сервис пользователей там будут храниться именно профили пользователей.
        var result = new MeResponse
        {
            FullName = new FullNameModel{
                Name = "Анатолий",
                Surname = "Востриков",
                Patronymic = "Александрович",
            },
            AvatarUrl = "https://localhost:7002/images/d6fbf57c-e050-49d0-bb32-453544e8e01b",
            UserName = "vostrikovanatoliy@gmail.com",
        };

        return GetOk(result);
    }

}