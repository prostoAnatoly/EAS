using AutoMapper;
using Identity.App.Features.Login;
using Identity.App.Features.Logout;
using Identity.App.Features.Registration;
using Identity.App.Features.ValidateToken;
using Identity.Grpc.Contracts.Arguments;
using Identity.Grpc.Contracts.Responses;

namespace Identity.Grpc.MappingProfiles;

class IdentityServiceMappingProfile : Profile
{

    public IdentityServiceMappingProfile()
    {
        MapLogin();
    }

    private void MapLogin()
    {
        CreateMap<LoginGrpcArgs, LoginCommand>();
        CreateMap<LoginResult, LoginGrpcResponse>();
        CreateMap<ValidateTokenResult, ValidateTokenGrpcResponse>();
        CreateMap<RegistrationResult, RegistrationGrpcResponse>();

        CreateMap<LogoutGrpcArgs, LogoutCommand>();
        CreateMap<ValidateTokenGrpcArgs, ValidateTokenQuery>();
        CreateMap<RegistrationGrpcArgs, RegistrationCommand>();
    }

}