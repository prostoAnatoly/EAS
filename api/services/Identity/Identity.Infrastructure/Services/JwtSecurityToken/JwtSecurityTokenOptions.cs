using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Identity.Infrastructure.Services.JwtSecurityToken;

public class JwtSecurityTokenOptions
{
    /// <summary>
    /// Список получателей токена
    /// </summary>
    public string? Audience { get; set; }

    /// <summary>
    /// Уникальный идентификатор издателя токена
    /// </summary>
    public string? Issuer { get; set; }

    /// <summary>
    /// Время жизни токена, в секундах.
    /// </summary>
    public long Lifetime { get; set; }

    /// <summary>
    /// Ключ для симметричного шифрования
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// Срок действия токена для обновления токета доступа, в днях
    /// </summary>
    public int RefreshTokenExpiration { get; set; }

    public SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
    }

}
