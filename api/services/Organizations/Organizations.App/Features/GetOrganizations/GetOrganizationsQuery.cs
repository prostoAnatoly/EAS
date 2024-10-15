using Shared.Mediator;

namespace Organizations.App.Features;

public class GetOrganizationsQuery : IQuery<GetOrganizationsResult>
{
    
    public required Guid OwnerId { get; init; }
}