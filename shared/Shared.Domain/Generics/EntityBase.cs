namespace Shared.Domain.Generics;

public abstract class EntityBase<TId> : Entity<TId> where TId : StronglyTypedId, new()
{
    /// <summary>
    /// Дата создания.
    /// </summary>
    public DateTimeOffset CreateAt { get; init; } = DateTimeOffset.UtcNow;
}
