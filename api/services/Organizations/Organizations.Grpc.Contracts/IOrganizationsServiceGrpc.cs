using Organizations.Grpc.Contracts.Arguments;
using Organizations.Grpc.Contracts.Responses;
using System.ServiceModel;
using ProtoBuf.Grpc;

namespace Organizations.Grpc.Contracts;

[ServiceContract]
public interface IOrganizationsServiceGrpc
{

    [OperationContract]
    Task<CreateOrganizationGrpcResponse> CreateOrganization(CreateOrganizationArgs args, CallContext context = default);

    [OperationContract]
    Task<GetOrganizationsGrpcResponse> GetOrganizations(GetOrganizationsArgs args, CallContext context = default);
}
