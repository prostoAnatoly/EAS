using Shared.Domain;

namespace Employees.Domain.ValueObjects;

public class Contact : ValueObject
{
    public static readonly Contact Empty = new() { Type = ContactType.None, Value = string.Empty };

    /// <summary>
    /// Значение контакта.
    /// </summary>
    public required string Value { get; init; }

    /// <summary>
    /// Тип контакта
    /// </summary>
    public required ContactType Type { get; init; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
        yield return Type;
    }
}

public enum ContactType
{
    None = 0,

    MobilePhoneNumber = 1,

    Email = 2,
}