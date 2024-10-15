using AutoMapper;
using Organizations.App.Dtos;
using Organizations.Domain.Aggregates.Organizations;

namespace Organizations.App.MappingProfiles;

class OrganizationsMappingProfile : Profile
{

    public OrganizationsMappingProfile()
    {
        CreateMap<Organization, OrganizationDto>()
            .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id.Value));
    }
}
