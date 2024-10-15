using Employees.Grpc.Contracts;
using Employees.Grpc.Contracts.Arguments;
using Employees.Grpc.Contracts.Responses;
using SeedWork.Infrastructure.Extensions;

namespace Employees.Sdk;

public sealed class EmployeesClient
{
    private readonly IEmployeesServiceGrpc _employeesServiceGrpc;

    public EmployeesClient(IEmployeesServiceGrpc employeesServiceGrpc)
    {
        _employeesServiceGrpc = employeesServiceGrpc;
    }

    public async Task<Guid> Create(CreateEmployeeGrpcArgs args)
    {
        return await _employeesServiceGrpc.Create(args).TryCatchGrpcToAppException();
    }

    public async Task<GetEmployeesGrpcResponse> GetEmployees(GetEmployeesGrpcArgs args)
    {
        return await _employeesServiceGrpc.GetEmployees(args).TryCatchGrpcToAppException();
    }
}