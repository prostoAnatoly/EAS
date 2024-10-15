using Shared.Domain.Generics;

namespace Identity.Domain.Aggregates.Identities;

public class IdentityInfo : Entity<IdentityId>
{

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public required string UserName { get; init; }

    /// <summary>
    /// Пароль пользователя.
    /// </summary>
    public string? Password { get; private set; }

    public void SetPassword(string value)
    {
        Password = value;
    }

}
