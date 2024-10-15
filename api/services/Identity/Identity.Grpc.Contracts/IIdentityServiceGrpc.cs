using Identity.Grpc.Contracts.Arguments;
using Identity.Grpc.Contracts.Responses;
using ProtoBuf.Grpc;
using System.ServiceModel;

namespace Identity.Grpc.Contracts;

[ServiceContract]
public interface IIdentityServiceGrpc
{

    [OperationContract]
    Task<LoginGrpcResponse> Login(LoginGrpcArgs args, CallContext context = default);

    [OperationContract]
    Task Logout(LogoutGrpcArgs args, CallContext context = default);

    [OperationContract]
    Task<RegistrationGrpcResponse> Registration(RegistrationGrpcArgs args, CallContext context = default);

    [OperationContract]
    Task<ValidateTokenGrpcResponse> ValidateToken(ValidateTokenGrpcArgs args, CallContext context = default);
}