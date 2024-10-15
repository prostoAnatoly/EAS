using ProtoBuf;
using System.Runtime.Serialization;

namespace Employees.Grpc.Contracts.Arguments;

[DataContract]
[CompatibilityLevel(CompatibilityLevel.Level300)]
public sealed class GetEmployeesGrpcArgs
{
    [DataMember(Order = 1)]
    public required Guid OrganizationId { get; init; }

    [DataMember(Order = 2)]
    public required int PageNumber { get; init; }

    [DataMember(Order = 3)]
    public required int PageSize { get; init; }

    [DataMember(Order = 4)]
    public required FilterByEmployeeGrpcArgs Filter {  get; init; }
}
