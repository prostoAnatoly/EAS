using Microsoft.EntityFrameworkCore;
using Npgsql;
using Shared.Persistence;

namespace SeedWork.Infrastructure.Persistence.RepositoryContextOptions;

sealed class RepositoryContextOptionsPostgreSql : IRepositoryContextOptions
{
    private readonly static int UniqueViolationCode = 23505;

    /// <inheritdoc/>
    public bool IsDbForeingKeyException(DbUpdateException dbUpdateException)
    {
        return dbUpdateException.InnerException is PostgresException postgresException
                   && postgresException.ErrorCode == UniqueViolationCode;
    }
}