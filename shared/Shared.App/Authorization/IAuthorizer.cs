namespace Shared.App.Authorization;

/// <summary>
/// Сервис выполняющий авторизацию.
/// </summary>
/// <typeparam name="TRequest">Тип команды/запроса.</typeparam>
public interface IAuthorizer<in TRequest>
{
    /// <summary>
    /// Проверка полномочий на выполнение команды/запроса.
    /// </summary>
    Task Authorize(TRequest request, IAuthorizationContext context, CancellationToken cancellationToken);
}
