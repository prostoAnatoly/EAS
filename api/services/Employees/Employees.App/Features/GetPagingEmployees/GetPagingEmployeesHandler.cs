using Employees.App.ReadModel.DbContexts;
using Employees.App.ReadModel.Models;
using SeedWork.Application.Features;
using Shared.Common.Models.TableFilters;

namespace Employees.App.Features.GetPagingEmployees;

public sealed record GetPagingEmployeesQuery : GetPagingBaseQuery<EmployeeRm>
{
    public required Guid OrganizationId { get; init; }
}

sealed class GetPagingEmployeesHandler : GetPagingBaseHandler<GetPagingEmployeesQuery, EmployeeRm>
{
    private readonly static ColumnInfo<EmployeeRm>[]? _columns =
        [
            new()
            {
                Filtering = (s, val) => s.Where(x => x.Name.Contains(val)),
                Name = nameof(EmployeeRm.Name),
                Sorting = null,
            },
            new()
            {
                Filtering = null,
                Name = nameof(EmployeeRm.State),
                Sorting = x => x.State,
            },
        ];

    public GetPagingEmployeesHandler(IReadModelsContext readModelsContext)
        : base(readModelsContext.EmployeeRms) { }

    protected override ColumnInfo<EmployeeRm>[]? Columns => _columns;

    protected override IQueryable<EmployeeRm> ApplyCustomFilters(IQueryable<EmployeeRm> items, GetPagingEmployeesQuery query)
    {
        return items
            .Where(x => x.OrganizationId == query.OrganizationId);
    }
}