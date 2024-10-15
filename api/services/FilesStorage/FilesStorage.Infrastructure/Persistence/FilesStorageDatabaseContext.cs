using FilesStorage.App.Infrastructure.DbContexts;
using FilesStorage.Domain.Aggregates.Files;
using Microsoft.EntityFrameworkCore;
using SeedWork.Infrastructure.Persistence;
using Shared.Persistence;

namespace FilesStorage.Infrastructure.Persistence
{
    public class FilesStorageDatabaseContext(DbContextOptions options, IRepositoryContextOptions repositoryContextOptions)
        : BaseDatabaseContext(options, repositoryContextOptions), IFilesStoragesContext
    {
        public DbSet<FileProps> FileProps { get; private set; }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FilesStorageDatabaseContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

    }
}
