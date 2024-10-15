using FilesStorage.Domain.Aggregates.Files;
using Microsoft.EntityFrameworkCore;

namespace FilesStorage.App.Infrastructure.DbContexts;

public interface IFilesStoragesContext
{

    public DbSet<FileProps> FileProps { get; }
}