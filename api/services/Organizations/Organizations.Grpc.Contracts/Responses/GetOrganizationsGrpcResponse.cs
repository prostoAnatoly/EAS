using Organizations.Grpc.Contracts.ResponseParts;
using ProtoBuf;
using System.Runtime.Serialization;

namespace Organizations.Grpc.Contracts.Responses;

[DataContract]
[CompatibilityLevel(CompatibilityLevel.Level300)]
public sealed class GetOrganizationsGrpcResponse
{

    private GetOrganizationsGrpcResponse() { }

    [DataMember(Order = 1)]
    public IEnumerable<OrganizationGrpc> Organizations { get; private init; } = [];
}
