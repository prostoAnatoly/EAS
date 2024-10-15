namespace Organizations.App.Dtos;

public record OrganizationDto()
{
    public required Guid Id { get; init; }

    public required string Name { get; init; }

    public required DateTimeOffset CreateAt { get; init; }
}