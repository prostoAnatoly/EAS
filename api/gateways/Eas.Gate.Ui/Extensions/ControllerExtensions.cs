using Eas.Gate.Ui.JwtToken;
using Microsoft.AspNetCore.Mvc;
using Shared.Rest.Common.Extensions;
using System.Security.Authentication;

namespace EasGateUi.Extensions;

/// <summary>
/// Расширение типа <see cref="ControllerBase"/>
/// </summary>
public static class ControllerExtensions
{

    /// <summary>
    /// Возвращает идентификатор авторизованного пользователя
    /// </summary>
    /// <param name="controller"></param>
    public static Guid GetRequiredUserId(this ControllerBase controller)
    {
        var value = controller.User.GetClaimValue(IdentityEnricher.ClaimNameUserId);

        if (Guid.TryParse(value, out Guid userId))
        {
            return userId;
        }

        throw new AuthenticationException("Пользователь не авторизован");
    }
}