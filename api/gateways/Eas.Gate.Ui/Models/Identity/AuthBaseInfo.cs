namespace EasGateUi.Models.Identity;

/// <summary>
/// Модель ответа для базовой, однофакторной аутентификации при получении Токена
/// </summary>
public class AuthBaseInfo
{

    /// <summary>
    /// Токен доступа
    /// </summary>
    /// <example>eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpYXQiOiIxNjIyNjk4NT</example>
    public string AccessToken { get; }

    /// <summary>
    /// Истекает, в секундах
    /// </summary>
    /// <example>3600</example>
    public long ExpiresIn { get; }

    /// <summary>
    /// Токена для обновления <see cref="AccessToken"/>
    /// </summary>
    /// <example>GEqDvujW6Tw2fC9nv79MwUMC2QHk2</example>
    public string RefreshToken { get; }

    public AuthBaseInfo(string accessToken, long expiresIn, string refreshToken)
    {
        AccessToken = accessToken;
        ExpiresIn = expiresIn;
        RefreshToken = refreshToken;
    }

}
