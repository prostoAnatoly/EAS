using Eas.Gate.Ui.Models.Common.Grid;
using Eas.Gate.Ui.Models.Employees;
using Employees.Grpc.Contracts.Arguments;
using Employees.Sdk;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Shared.Rest.Common.Controllers;
using System.ComponentModel.DataAnnotations;

namespace Eas.Gate.Ui.Controllers;

/// <summary>
/// API по работе с сотрудниками
/// </summary>
[Route("api/employees/{organizationId:Guid}")]
[ApiVersion("1.0")]
[EnableCors(CorsPolicyName.MAIN_POLICY)]
[Authorize()]
public class EmployeesController : ApiControllerBase
{
    private readonly EmployeesClient _employeesClient;

    private static readonly List<EmployeeModel> _employees = GetEmployeeModels();

    public EmployeesController(EmployeesClient employeesClient)
    {
        _employeesClient = employeesClient;
    }

    [HttpPost("search")]
    public IActionResult GetItems([FromRoute, Required] Guid organizationId, EmployeeItemsSearchModel searchModel)
    {
        var emps = _employees
                .Where(x => searchModel.State == null || (searchModel.State != null && x.State == searchModel.State))
                .ToArray();
        var result = new PaginationModel<EmployeeModel>
        {
            Items = emps
                .Skip(searchModel.PageSize * (searchModel.Page - 1))
                .Take(searchModel.PageSize)
                .ToArray(),
            Total = emps.Length,
            TotalPages = emps.Length / searchModel.PageSize + 1,
        };

        return GetOk(result);
    }

    [HttpPost()]
    public async Task<Guid> Create([FromRoute, Required] Guid organizationId, CreateEmployeeModel employeeModel)
    {
        var args = new CreateEmployeeGrpcArgs
        {
            OrganizationId = organizationId,
            MobilePhoneNumber = employeeModel.PhoneNumber,
            Name = employeeModel.FullName.Name,
            Surname = employeeModel.FullName.Surname,
            Patronymic = employeeModel.FullName.Patronymic,
            Birthday = employeeModel.Birthday,
            EmploymentDate = employeeModel.EmploymentDate,
            Email = employeeModel.Email,
        };

        var employeeId = await _employeesClient.Create(args);

        return employeeId;
    }

    [HttpPut("{employeeId:guid}")]
    public IActionResult Update([FromRoute, Required] Guid organizationId, [FromRoute, Required] Guid employeeId,
        UpdateEmployeeModel employeeModel)
    {
        var employee = _employees.First(x => x.Id == employeeId);

        employee.Birthday = employeeModel.Birthday;
        employee.Email = employeeModel.Email;
        employee.FullName = employeeModel.FullName;
        employee.PhoneNumber = employeeModel.PhoneNumber;
        employee.EmploymentDate = employeeModel.EmploymentDate;

        return GetOk(employee);
    }

    [HttpGet("{employeeId:guid}")]
    public IActionResult Get([FromRoute, Required] Guid organizationId, [FromRoute, Required] Guid employeeId)
    {
        var employee = _employees.First(x => x.Id == employeeId);

        return GetOk(employee);
    }

    private static List<EmployeeModel> GetEmployeeModels()
    {
        return
        [
            new EmployeeModel
            {
                Id = Guid.NewGuid(),
                Birthday = DateTime.Now,
                Email = "test@ts.com",
                FullName = new Models.Common.FullNameModel
                {
                    Name = "Test",
                    Patronymic = string.Empty,
                    Surname = "Тестов",
                },
                PhoneNumber = "79231524574",
                State = EmployeeState.Active,
                EmploymentDate = DateTime.Now,
            },
            new EmployeeModel
            {
                Id = Guid.NewGuid(),
                Birthday = DateTime.Now,
                Email = "test1@ts.com",
                FullName = new Models.Common.FullNameModel
                {
                    Name = "Ivan",
                    Patronymic = string.Empty,
                    Surname = "Тестов",
                },
                PhoneNumber = "79231524574",
                State = EmployeeState.Active,
                EmploymentDate = DateTime.Now,
            },
            new EmployeeModel
            {
                Id = Guid.NewGuid(),
                Birthday = DateTime.Now,
                Email = "test3@ts.com",
                FullName = new Models.Common.FullNameModel
                {
                    Name = "Yily",
                    Patronymic = string.Empty,
                    Surname = "Тестов",
                },
                PhoneNumber = "79231524574",
                State = EmployeeState.Active,
                EmploymentDate = DateTime.Now,
            },
            new EmployeeModel
            {
                Id = Guid.NewGuid(),
                Birthday = DateTime.Now,
                Email = "test3@ts.com",
                FullName = new Models.Common.FullNameModel
                {
                    Name = "Yily",
                    Patronymic = string.Empty,
                    Surname = "Тестов",
                },
                PhoneNumber = "79231524574",
                State = EmployeeState.Active,
                EmploymentDate = DateTime.Now,
            },
            new EmployeeModel
            {
                Id = Guid.NewGuid(),
                Birthday = DateTime.Now,
                Email = "test3@ts.com",
                FullName = new Models.Common.FullNameModel
                {
                    Name = "Yily",
                    Patronymic = string.Empty,
                    Surname = "Тестов",
                },
                PhoneNumber = "79231524574",
                State = EmployeeState.Active,
                EmploymentDate = DateTime.Now,
            },
            new EmployeeModel
            {
                Id = Guid.NewGuid(),
                Birthday = DateTime.Now,
                Email = "test3@ts.com",
                FullName = new Models.Common.FullNameModel
                {
                    Name = "Yily",
                    Patronymic = string.Empty,
                    Surname = "Тестов",
                },
                PhoneNumber = "79231524574",
                State = EmployeeState.Active,
                EmploymentDate = DateTime.Now,
            },
            new EmployeeModel
            {
                Id = Guid.NewGuid(),
                Birthday = DateTime.Now,
                Email = "test3@ts.com",
                FullName = new Models.Common.FullNameModel
                {
                    Name = "Yily",
                    Patronymic = string.Empty,
                    Surname = "Тестов",
                },
                PhoneNumber = "79231524574",
                State = EmployeeState.Dismissed,
                EmploymentDate = DateTime.Now,
            },
            new EmployeeModel
            {
                Id = Guid.NewGuid(),
                Birthday = DateTime.Now,
                Email = "test3@ts.com",
                FullName = new Models.Common.FullNameModel
                {
                    Name = "Yily",
                    Patronymic = string.Empty,
                    Surname = "Тестов",
                },
                PhoneNumber = "79231524574",
                State = EmployeeState.Dismissed,
                EmploymentDate = DateTime.Now,
            },
            new EmployeeModel
            {
                Id = Guid.NewGuid(),
                Birthday = DateTime.Now,
                Email = "test3@ts.com",
                FullName = new Models.Common.FullNameModel
                {
                    Name = "Yily",
                    Patronymic = string.Empty,
                    Surname = "Тестов",
                },
                PhoneNumber = "79231524574",
                State = EmployeeState.Dismissed,
                EmploymentDate = DateTime.Now,
            }
        ];
    }
}
