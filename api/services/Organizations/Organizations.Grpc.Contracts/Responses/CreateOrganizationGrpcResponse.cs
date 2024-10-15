using Organizations.Grpc.Contracts.ResponseParts;
using ProtoBuf;
using System.Runtime.Serialization;

namespace Organizations.Grpc.Contracts.Responses;

[DataContract]
[CompatibilityLevel(CompatibilityLevel.Level300)]
public sealed class CreateOrganizationGrpcResponse
{

    private CreateOrganizationGrpcResponse() { }

    [DataMember(Order = 1)]
    public OrganizationGrpc Organization { get; private init; }
}
