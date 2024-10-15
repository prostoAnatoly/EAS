using ProtoBuf;
using System.Runtime.Serialization;

namespace Employees.Grpc.Contracts.ResponseParts;

[DataContract]
[CompatibilityLevel(CompatibilityLevel.Level300)]
public sealed class EmployeeGrpc
{
    private EmployeeGrpc() { }
}
