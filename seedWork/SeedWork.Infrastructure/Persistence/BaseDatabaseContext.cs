using Microsoft.EntityFrameworkCore;
using Shared.Domain.Exceptions;
using Shared.Persistence;

namespace SeedWork.Infrastructure.Persistence;

public abstract class BaseDatabaseContext : BaseDbContext
{
    protected BaseDatabaseContext(DbContextOptions options,
        IRepositoryContextOptions repositoryContextOptions) : base(options, repositoryContextOptions) { }

    protected override DbForeignKeyException GetDbForeingKeyException(DbUpdateException ex)
    {
        return new DbForeignKeyException("Сущность уже существует", ex);
    }
}
