namespace Shared.Domain.Generics;

public abstract class Entity<TId> : Entity, IEquatable<Entity<TId>> where TId : StronglyTypedId, new()
{
    public TId Id { get; }

    protected Entity()
    {
        Id = StronglyTypedId.Create<TId>(Guid.NewGuid());
    }

    public bool Equals(Entity<TId>? other)
    {
        if (other is null) { return false; }

        if (ReferenceEquals(this, other)) { return true; }

        return EqualityComparer<TId>.Default.Equals(Id, other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) { return false; }

        if (ReferenceEquals(this, obj)) { return true; }

        if (obj.GetType() != GetType()) { return false; }

        return Equals((Entity<TId>)obj);
    }

    public override int GetHashCode()
    {
        return EqualityComparer<TId>.Default.GetHashCode(Id);
    }

    public static bool operator ==(Entity<TId>? left, Entity<TId>? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Entity<TId>? left, Entity<TId>? right)
    {
        return !Equals(left, right);
    }

}