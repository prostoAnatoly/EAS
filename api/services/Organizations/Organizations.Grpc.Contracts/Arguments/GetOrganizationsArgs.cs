using ProtoBuf;
using System.Runtime.Serialization;

namespace Organizations.Grpc.Contracts.Arguments;

[DataContract]
[CompatibilityLevel(CompatibilityLevel.Level300)]
public class GetOrganizationsArgs
{
    /// <summary>
    /// Идентификатор владельца организации.
    /// </summary>
    [DataMember(Order = 1)]
    public required Guid OwnerId { get; init; }
}
