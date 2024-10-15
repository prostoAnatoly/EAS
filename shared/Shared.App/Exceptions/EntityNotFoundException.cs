using Shared.Domain;

namespace Shared.App.Exceptions;

public sealed class EntityNotFoundException : ApplicationLogicException
{
    public StronglyTypedId? EntityId { get; }

    public EntityNotFoundException(string? message) : base(message) { }

    public EntityNotFoundException(string message, StronglyTypedId entityId) : this(message)
    {
        EntityId = entityId;
    }
}
