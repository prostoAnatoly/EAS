using AutoMapper;
using Organizations.App.Dtos;
using Organizations.App.Features;
using Organizations.Grpc.Contracts.Arguments;
using Organizations.Grpc.Contracts.ResponseParts;
using Organizations.Grpc.Contracts.Responses;

namespace Organizations.Grpc.MappingProfiles;

sealed class OrganizationsServiceMappingProfile : Profile
{

    public OrganizationsServiceMappingProfile()
    {
        MapParts();

        MapCreateOrganization();
        MapGetOrganizations();
    }

    private void MapParts()
    {
        CreateMap<OrganizationDto, OrganizationGrpc>();
    }

    private void MapCreateOrganization()
    {
        CreateMap<CreateOrganizationArgs, CreateOrganizationCommand>();
        CreateMap<CreateOrganizationResult, CreateOrganizationGrpcResponse>();
    }

    private void MapGetOrganizations()
    {
        CreateMap<GetOrganizationsArgs, GetOrganizationsQuery>();
        CreateMap<GetOrganizationsResult, GetOrganizationsGrpcResponse>();
    }
}