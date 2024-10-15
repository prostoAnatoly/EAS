using Employees.Grpc.Contracts.Arguments;
using Employees.Grpc.Contracts.Responses;
using ProtoBuf.Grpc;
using System.ServiceModel;

namespace Employees.Grpc.Contracts;

[ServiceContract]
public interface IEmployeesServiceGrpc
{

    [OperationContract]
    Task<Guid> Create(CreateEmployeeGrpcArgs args, CallContext context = default);

    [OperationContract]
    Task ChangePersonalInfo(CreateEmployeeGrpcArgs args, CallContext context = default);

    [OperationContract]
    Task ChangeContacts(CreateEmployeeGrpcArgs args, CallContext context = default);

    [OperationContract]
    Task<GetEmployeesGrpcResponse> GetEmployees(GetEmployeesGrpcArgs args, CallContext callContext = default);
}
