using Organizations.App.Dtos;

namespace Organizations.App.Features;

public record GetOrganizationsResult(IEnumerable<OrganizationDto> Organizations);