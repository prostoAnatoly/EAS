using AutoMapper;
using Organizations.App.Dtos;
using Organizations.App.Infrastructure.Repositories;
using Organizations.Domain.Aggregates.Organizations;
using Organizations.Domain.ValueObjects;
using Shared.Domain.Exceptions;
using Shared.Mediator;

namespace Organizations.App.Features;

sealed class CreateOrganizationHandler : ICommandHandler<CreateOrganizationCommand, CreateOrganizationResult>
{
    private const int MAX_COUNT_ORGANIZATIONS = 5;

    private readonly IOrganizationsRepository _organizationRepository;
    private readonly IMapper _mapper;

    public CreateOrganizationHandler(IOrganizationsRepository organizationRepository, IMapper mapper)
    {
        _organizationRepository = organizationRepository;
        _mapper = mapper;
    }

    public async Task<CreateOrganizationResult> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
    {
        var ownerId = new UserId(request.CreatorId);
        var count = await _organizationRepository.GetCountByOwner(ownerId, cancellationToken);
        if (count >= MAX_COUNT_ORGANIZATIONS)
        {
            throw new DomainException($"Допустимо только {MAX_COUNT_ORGANIZATIONS} организаций");
        }

        var creatorId = new UserId(request.CreatorId);
        var organization = Organization.Create(request.Name, ownerId, creatorId);

        _organizationRepository.AddOrganization(organization);

        return new CreateOrganizationResult(_mapper.Map<OrganizationDto>(organization));
    }
}