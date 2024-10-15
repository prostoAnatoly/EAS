using Shared.Common.IO;

namespace FilesStorage.App.Features;

public class DownloadResult
{

    public string FileName { get; }

    public AsyncStream FileStream { get; }

    public DownloadResult(AsyncStream fileStream, string fileName)
    {
        FileStream = fileStream;
        FileName = fileName;
    }

}