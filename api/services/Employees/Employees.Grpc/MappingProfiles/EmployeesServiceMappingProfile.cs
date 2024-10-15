using AutoMapper;
using Employees.App.Dtos;
using Employees.App.Features;
using Employees.Grpc.Contracts.Arguments;

namespace Employees.Grpc.MappingProfiles;

sealed class EmployeesServiceMappingProfile : Profile
{

    public EmployeesServiceMappingProfile()
    {
        CreateMap<CreateEmployeeGrpcArgs, CreateEmployeeCommand>();
        CreateMap<FilterByEmployeeGrpcArgs, FilterByEmployeeDto>();
    }
}