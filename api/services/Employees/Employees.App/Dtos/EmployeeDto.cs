namespace Employees.App.Dtos;

public record EmployeeDto
{

    public required Guid Id { get; init; }

    public required  Guid OrganizationId { get; init; }

    public required FullNameDto FullName { get; init; }

    public ContactInfoDto ContactInfo { get; init; }

    public required DateTimeOffset Birthday { get; init; }

    public required DateTimeOffset EmploymentDate { get; init; }

    public DateTimeOffset? DateOfDismissal { get; init; }
}