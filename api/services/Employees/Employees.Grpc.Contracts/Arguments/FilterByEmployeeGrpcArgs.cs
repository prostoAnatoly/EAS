using Employees.Grpc.Contracts.Common;
using ProtoBuf;
using System.Runtime.Serialization;

namespace Employees.Grpc.Contracts.Arguments;

[DataContract]
[CompatibilityLevel(CompatibilityLevel.Level300)]
public class FilterByEmployeeGrpcArgs
{

    [DataMember(Order = 1)]
    public required EmployeeStateGrpc State { get; init; }
}
