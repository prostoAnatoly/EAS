using Microsoft.EntityFrameworkCore;
using Shared.Domain.Exceptions;

namespace Shared.Persistence;

public abstract class BaseDbContext : DbContext
{
    private readonly IRepositoryContextOptions? repositoryContextOptions;

    protected BaseDbContext(DbContextOptions options, IRepositoryContextOptions? repositoryContextOptions = null)
        : base(options)
    {
        this.repositoryContextOptions = repositoryContextOptions;
    }

    protected virtual DbForeignKeyException GetDbForeingKeyException(DbUpdateException ex)
    {
        return new DbForeignKeyException("Вставка или обновление в таблице нарушает ограничение внешнего ключа. Детальнее см. внутреннее исключение", ex);
    }

    private bool IsDbForeingKeyException(DbUpdateException ex)
    {
        return repositoryContextOptions?.IsDbForeingKeyException(ex) == true;
    }

    /// <inheritdoc />
    public override int SaveChanges()
    {
        try
        {
            return base.SaveChanges();
        }
        catch (DbUpdateException ex) when (IsDbForeingKeyException(ex))
        {
            throw GetDbForeingKeyException(ex);
        }
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        try
        {
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        catch (DbUpdateException ex) when (IsDbForeingKeyException(ex))
        {
            throw GetDbForeingKeyException(ex);
        }
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        try
        {
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        catch (DbUpdateException ex) when (IsDbForeingKeyException(ex))
        {
            throw GetDbForeingKeyException(ex);
        }
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return base.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex) when (IsDbForeingKeyException(ex))
        {
            throw GetDbForeingKeyException(ex);
        }
    }
}