using Organizations.Domain.Aggregates.Organizations;

namespace Organizations.Domain.SizeFields;

/// <summary>
/// Размер полей для <see cref="Organization"/>
/// </summary>
public class OrganizationSizes
{

    /// <summary>
    /// Размер значения поля <see cref="Organization.Name"/>
    /// </summary>
    public const int NAME = 255;

}