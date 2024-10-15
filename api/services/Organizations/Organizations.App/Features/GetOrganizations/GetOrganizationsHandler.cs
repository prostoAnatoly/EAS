using AutoMapper;
using Organizations.App.Dtos;
using Organizations.App.Infrastructure.Repositories;
using Organizations.Domain.ValueObjects;
using Shared.Mediator;

namespace Organizations.App.Features;

sealed class GetOrganizationsHandlerHandler : IQueryHandler<GetOrganizationsQuery, GetOrganizationsResult>
{
    private readonly IOrganizationsRepository _organizationRepository;
    private readonly IMapper _mapper;

    public GetOrganizationsHandlerHandler(IOrganizationsRepository organizationRepository, IMapper mapper)
    {
        _organizationRepository = organizationRepository;
        _mapper = mapper;
    }

    public async Task<GetOrganizationsResult> Handle(GetOrganizationsQuery request, CancellationToken cancellationToken)
    {
        var ownerId = new UserId(request.OwnerId);
        var orgs = await _organizationRepository.GetAll(ownerId, cancellationToken);

        var organizationDtos = _mapper.Map<IEnumerable<OrganizationDto>>(orgs);
        
        return new GetOrganizationsResult(organizationDtos);
    }

}
