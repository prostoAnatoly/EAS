using Microsoft.EntityFrameworkCore;
using Shared.Common.Extensions;
using Shared.Common.Models.Paging;
using Shared.Common.Models.TableFilters;
using Shared.Mediator;
using System.Linq.Expressions;

namespace SeedWork.Application.Features;

public abstract record GetPagingBaseQuery<TItem> : IQuery<Pagination<TItem>>
{
    public required int PageNumber { get; init; }

    public required int PageSize { get; init; }

    public IEnumerable<ColumnFilter>? Filters { get; init; }

    public IEnumerable<ColumnSort>? Sorts { get; init; }
}

public abstract class GetPagingBaseHandler<TQuery, TItem> : IQueryHandler<TQuery, Pagination<TItem>>
    where TQuery : GetPagingBaseQuery<TItem>
    where TItem : class, IReadModelWithId
{
    private readonly DbSet<TItem> _dbSet;
    private static readonly Expression<Func<TItem, object>> defaultOrder = x => x.Id;

    protected GetPagingBaseHandler(DbSet<TItem> dbSet)
    {
        _dbSet = dbSet;
    }

    protected virtual ColumnInfo<TItem>[]? Columns { get; }

    protected abstract IQueryable<TItem> ApplyCustomFilters(IQueryable<TItem> items, TQuery query);

    public async Task<Pagination<TItem>> Handle(TQuery request, CancellationToken cancellationToken)
    {
        var items = ApplyCustomFilters(_dbSet.AsNoTracking(), request);

        items = items
            .Filtration(Columns, request.Filters)
            .Sorting(Columns, request.Sorts, defaultOrder);

        var totalCount = await items.CountAsync(cancellationToken);

        var offset = request.PageSize * (request.PageNumber - 1);
        var pagedEmployees = await items
            .Paging(offset, request.PageSize)
            .ToArrayAsync(cancellationToken);

        return new Pagination<TItem>(totalCount, pagedEmployees);
    }
}