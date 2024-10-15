using Employees.Grpc.Contracts.ResponseParts;
using ProtoBuf;
using System.Runtime.Serialization;

namespace Employees.Grpc.Contracts.Responses;

[DataContract]
[CompatibilityLevel(CompatibilityLevel.Level300)]
public sealed class GetEmployeesGrpcResponse
{

    private GetEmployeesGrpcResponse() { }

    [DataMember(Order = 1)]
    public int TotalCount { get; init; }

    [DataMember(Order = 2)]
    public IEnumerable<EmployeeGrpc> Employees { get; init; } = [];
}
