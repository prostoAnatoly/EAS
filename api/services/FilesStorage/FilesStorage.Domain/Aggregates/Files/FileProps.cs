using Shared.Domain.Generics;

namespace FilesStorage.Domain.Aggregates.Files
{
    public class FileProps : Entity<FileId>
    {

        public required string FileName { get; init; }
    }
}
