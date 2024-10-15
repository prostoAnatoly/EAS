namespace Shared.Common.Models.Paging;

public sealed record Pagination<T>(int TotalCount, T[] Items);
