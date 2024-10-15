
namespace Shared.Domain;

public abstract class StronglyTypedId: IEquatable<StronglyTypedId>, IComparable<StronglyTypedId>, IComparable
{
    public Guid Value { get; private set; }

    protected StronglyTypedId()
    {
    }

    protected StronglyTypedId(Guid value) : this()
    {
        Value = value;
    }

    public static T Create<T>(Guid value)
        where T : StronglyTypedId, new()
    {
        var id = new T
        {
            Value = value
        };

        return id;
    }

    public static implicit operator Guid(StronglyTypedId id) => id.Value;

    public bool Equals(StronglyTypedId? other)
    {
        if (other is null) { return false; }
        
        if (ReferenceEquals(this, other)) { return true; }

        if (!(other.GetType().IsInstanceOfType(this) || GetType().IsInstanceOfType(other))) { return false; }

        return Value.Equals(other.Value);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) { return false; }
        
        if (ReferenceEquals(this, obj)) { return true; }

        if (!(obj.GetType().IsInstanceOfType(this) || GetType().IsInstanceOfType(obj))) { return false; }

        return Equals((StronglyTypedId) obj);
    }

    public static bool operator ==(StronglyTypedId? left, StronglyTypedId? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(StronglyTypedId? left, StronglyTypedId? right)
    {
        return !Equals(left, right);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public int CompareTo(StronglyTypedId? other)
    {
        return Value.CompareTo(other?.Value);
    }

    public int CompareTo(object? obj)
    {
        if (obj is null) { return 1; }
        if (ReferenceEquals(this, obj)) { return 0; }

        return obj is StronglyTypedId other
            ? CompareTo(other)
            : throw new ArgumentException($"Object must be of type {nameof(StronglyTypedId)}");
    }

    public override string ToString()
    {
        return Value.ToString();
    }

}