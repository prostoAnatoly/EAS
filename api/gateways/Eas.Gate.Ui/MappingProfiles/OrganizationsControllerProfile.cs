using AutoMapper;
using Eas.Gate.Ui.Models.Organizations;
using Organizations.Grpc.Contracts.ResponseParts;
using Organizations.Grpc.Contracts.Responses;

namespace Eas.Gate.Ui.MappingProfiles;

public class OrganizationsControllerProfile : Profile
{

    public OrganizationsControllerProfile()
    {
        CreateMap<OrganizationGrpc, OrganizationModel>();
        CreateMap<CreateOrganizationGrpcResponse, CreateOrganizationResponse>();
        CreateMap<GetOrganizationsGrpcResponse, GetOrganizationsResponse>();
    }
}
