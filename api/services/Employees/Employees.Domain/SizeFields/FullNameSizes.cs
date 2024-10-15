using Employees.Domain.ValueObjects;

namespace Employees.Domain.SizeFields;

/// <summary>
/// Размер полей для <see cref="FullName"/>
/// </summary>
public class FullNameSizes
{

    /// <summary>
    /// Размер значения поля <see cref="FullName.Name"/>
    /// </summary>
    public const int NAME = 50;

    /// <summary>
    /// Размер значения поля <see cref="FullName.Surname"/>
    /// </summary>
    public const int SURNAME = 50;

    /// <summary>
    /// Размер значения поля <see cref="FullName.Patronymic"/>
    /// </summary>
    public const int PATRONYMIC = 50;
}