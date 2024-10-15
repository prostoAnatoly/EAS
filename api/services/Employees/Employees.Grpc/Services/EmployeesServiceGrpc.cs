using Employees.App.Features;
using Employees.Domain.Aggregates.Employees;
using Employees.Grpc.Contracts;
using Employees.Grpc.Contracts.Arguments;
using Employees.Grpc.Contracts.Responses;
using ProtoBuf.Grpc;
using SeedWork.Grpc;
using SeedWork.Infrastructure.Helpers;

namespace Employees.Grpc.Services;

sealed class EmployeesServiceGrpc : BaseServiceGrpc, IEmployeesServiceGrpc
{

    public EmployeesServiceGrpc(MediatorHelper mediatorHelper) : base(mediatorHelper) { }

    public Task ChangeContacts(CreateEmployeeGrpcArgs args, CallContext context = default)
    {
        throw new NotImplementedException();
    }

    public Task ChangePersonalInfo(CreateEmployeeGrpcArgs args, CallContext context = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> Create(CreateEmployeeGrpcArgs args, CallContext context = default)
    {
        return await Run<CreateEmployeeCommand, EmployeeId>(args, context);
    }

    public Task<GetEmployeesGrpcResponse> GetEmployees(GetEmployeesGrpcArgs args, CallContext callContext = default)
    {
        throw new NotImplementedException();
    }
}
