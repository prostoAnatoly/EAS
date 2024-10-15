using ProtoBuf;
using System.Runtime.Serialization;

namespace Organizations.Grpc.Contracts.ResponseParts;

[DataContract]
[CompatibilityLevel(CompatibilityLevel.Level300)]
public class OrganizationGrpc()
{
    [DataMember(Order = 1)]
    public required Guid Id { get; init; }

    [DataMember(Order = 2)]
    public required string Name { get; init; }

    [DataMember(Order = 3)]
    public required DateTimeOffset CreateAt { get; init; }
}