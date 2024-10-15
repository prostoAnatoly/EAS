namespace Shared.Rest.Common.Models;

/// <summary>
/// Базовый класс, описывающий сущность, которая создаётся в API-методе 
/// </summary>
/// <typeparam name="TPayload">Тип полезной загрузки</typeparam>
public class EntityOfCreate<TPayload>
    where TPayload : class
{

    /// <summary>
    /// Универсальный уникальный идентификатор
    /// </summary>
    public Uuid Uuid { get; }

    /// <summary>
    /// Имя маршрута, используемое для URL-адреса возврата созданного ресурса
    /// </summary>
    public string RouteName { get; }

    /// <summary>
    /// Список значений для маршрута
    /// </summary>
    public object RouteValues { get; }

    /// <summary>
    /// Полезная нагрузка возвращаемая клиенту
    /// </summary>
    public TPayload Payload { get; }

    public EntityOfCreate(Uuid uuid, string routeName, object routeValues, TPayload payload)
    {
        Uuid = uuid;
        RouteName = routeName;
        RouteValues = routeValues;
        Payload = payload;
    }
}