using Shared.Domain;

namespace SeedWork.Domainn.ValueObjects;

/// <summary>
/// ФИО.
/// </summary>
public class FullName : ValueObject
{
    /// <summary>
    /// Имя.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Фамилия.
    /// </summary>
    public required string Surname { get; init; }

    /// <summary>
    /// Отчество.
    /// </summary>
    public string? Patronymic { get; init; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Name;
        yield return Surname;
        yield return Patronymic;
    }
}
