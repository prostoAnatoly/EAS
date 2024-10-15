using FilesStorage.Domain.Aggregates.Files;
using Shared.Common.IO;

namespace FilesStorage.App.Infrastructure.Services;

public interface IFilesStorageService
{
    Task Save(FileId fileId, AsyncStream asyncStream);

    AsyncStream GetFile(FileId fileId);

    void Delete(FileId fileId);
}