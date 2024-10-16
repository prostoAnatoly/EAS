namespace SeedWork.Application.Authorization;

/// <summary>
/// Сервис выполняющий авторизацию.
/// </summary>
/// <typeparam name="TRequest">Тип команды/запроса.</typeparam>
public interface IAuthorizer<in TRequest>
{
    IAuthorizationContext AuthorizationContext { get; }

    /// <summary>
    /// Проверка полномочий на выполнение команды/запроса.
    /// </summary>
    Task Authorize(TRequest request, IAuthorizationContext context, CancellationToken cancellationToken);
}
