namespace Identity.App.Features.ValidateToken;

public class ValidateTokenResult
{

    internal readonly static ValidateTokenResult Empty = new() { IsValid = false };

    /// <summary>
    /// Возвращает индикатор,показывающий, что токен доступа корретный.
    /// </summary>
    public required bool IsValid { get; init; }

    public Guid? IdentityId { get; init; } = null;

}