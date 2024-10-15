using ProtoBuf;
using System.Runtime.Serialization;

namespace Organizations.Grpc.Contracts.Arguments;

[DataContract]
[CompatibilityLevel(CompatibilityLevel.Level300)]
public class CreateOrganizationArgs
{
    /// <summary>
    /// Наименование организации.
    /// </summary>
    [DataMember(Order = 1)]
    public required string Name { get; init; }

    /// <summary>
    /// Идентификатор создателя организации.
    /// </summary>
    [DataMember(Order = 2)]
    public required Guid CreatorId { get; init; }
}
