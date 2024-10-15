namespace Shared.Domain;

/// <summary>
/// Базовый класс объектов-значений
/// </summary>
public abstract class ValueObject : IEquatable<ValueObject>
{
    protected abstract IEnumerable<object?> GetEqualityComponents();

    public bool Equals(ValueObject? other)
    {
        if (other is null) { return false; }
        
        if (ReferenceEquals(this, other)) { return true; }

        if (GetType() != other.GetType()) { return false; }

        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) { return false; }
        
        if (ReferenceEquals(this, obj)) { return true; }

        if (obj.GetType() != GetType()) { return false; }
        
        return Equals((ValueObject) obj);
    }

    public override int GetHashCode() => CombineHashCodes(GetEqualityComponents());

    private static int CombineHashCodes(IEnumerable<object?> collection)
    {
        unchecked
        {
            var hash = 17;
            foreach (var obj in collection)
            {
                hash = hash * 23 + (obj?.GetHashCode() ?? 0);
            }

            return hash;
        }
    }

    public static bool operator ==(ValueObject? left, ValueObject? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(ValueObject? left, ValueObject? right)
    {
        return !Equals(left, right);
    }
}