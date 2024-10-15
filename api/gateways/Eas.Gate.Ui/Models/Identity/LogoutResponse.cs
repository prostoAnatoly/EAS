namespace Eas.Gate.Ui.Models.Identity;

/// <summary>
/// Модель ответа выхода из системы
/// </summary>
public sealed class LogoutResponse
{
    /// <summary>
    /// Возвращает индикатор успешности выхода из системы
    /// </summary>
    /// <remarks>
    /// <see langword="true" /> если успешно; иначе <see langword="false" />
    /// </remarks>
    public bool IsOk { get; }

    public LogoutResponse(bool isOk)
    {
        IsOk = isOk;
    }
}
