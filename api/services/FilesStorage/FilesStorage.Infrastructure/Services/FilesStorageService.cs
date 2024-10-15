using FilesStorage.App.Infrastructure.Services;
using FilesStorage.Domain.Aggregates.Files;
using Shared.Common.IO;

namespace FilesStorage.Infrastructure.Services;

class FilesStorageService : IFilesStorageService
{
    private static readonly string pathToRootFolder = "DataStorage";
    private static readonly int chunkSize = 4096;

    public AsyncStream GetFile(FileId fileId)
    {
        var path = GetPath(fileId);
        var content = GetContentFile(path);

        return new AsyncStream(content);
    }

    public async Task Save(FileId fileId, AsyncStream asyncStream)
    {
        // Получение пути до папки
        var folder = GetPathToFolder(fileId);
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        // Получение полного пути к файлу
        var path = GetPath(fileId);

        // Сохраняем на диск файл
        using var stream = new FileStream(path, FileMode.Create, FileAccess.Write);
        await foreach(var chunk in asyncStream.Content)
        {
            await stream.WriteAsync(chunk);
        }
    }

    public void Delete(FileId fileId)
    {
        var path = GetPath(fileId);

        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    /// <summary>
    /// Возвращает полный путь до файла
    /// </summary>
    /// <param name="id">ИД файла</param>
    private static string GetPath(FileId id)
    {
        return Path.Combine(GetPathToFolder(id), GetFileName(id));
    }

    /// <summary>
    /// Возвращает полное имя файла
    /// </summary>
    /// <param name="id">ИД файла</param>
    private static string GetFileName(FileId id)
    {
        return $"{id}.bfile";
    }

    /// <summary>
    /// Возвращает путь до папки
    /// </summary>
    /// <param name="id">ИД файла</param>
    private static string GetPathToFolder(FileId fileId)
    {
        var pathSubDir = fileId.ToString()[..2];

        return Path.Combine(pathToRootFolder, pathSubDir);
    }

    private static async IAsyncEnumerable<byte[]> GetContentFile(string path)
    {
        var buffer = new byte[chunkSize];
        int read;

        using var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
        while ((read = await stream.ReadAsync(buffer)) > 0)
        {
            if (read < chunkSize)
            {
                Array.Resize(ref buffer, read);
            }

            yield return buffer;
        }
    }

}
