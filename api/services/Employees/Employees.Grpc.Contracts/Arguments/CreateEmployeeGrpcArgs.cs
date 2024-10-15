using ProtoBuf;
using System.Runtime.Serialization;

namespace Employees.Grpc.Contracts.Arguments;

[DataContract]
[CompatibilityLevel(CompatibilityLevel.Level300)]
public class CreateEmployeeGrpcArgs
{

    [DataMember(Order = 1)]
    public required Guid OrganizationId { get; init; }

    [DataMember(Order = 2)]
    public required string Name { get; init; }

    [DataMember(Order = 3)]
    public required string Surname { get; init; }

    [DataMember(Order = 4)]
    public string? Patronymic { get; init; }

    [DataMember(Order = 5)]
    public string? MobilePhoneNumber { get; init; }

    [DataMember(Order = 6)]
    public string? Email { get; init; }

    [DataMember(Order = 7)]
    public required DateTimeOffset Birthday { get; init; }

    [DataMember(Order = 8)]
    public required DateTimeOffset EmploymentDate { get; init; }
}
