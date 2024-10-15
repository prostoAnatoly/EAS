namespace Identity.App.Features.Login;

public class LoginResult
{

    /// <summary>
    /// Токен доступа
    /// </summary>
    public string AccessToken { get; }

    /// <summary>
    /// Истекает, в секундах
    /// </summary>
    public long ExpiresIn { get; }

    /// <summary>
    /// Токена для обновления <see cref="AccessToken"/>
    /// </summary>л
    public string RefreshToken { get; }

    public LoginResult(string accessToken, long expiresIn, string refreshToken)
    {
        AccessToken = accessToken;
        ExpiresIn = expiresIn;
        RefreshToken = refreshToken;
    }
}