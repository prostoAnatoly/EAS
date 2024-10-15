using Shared.Mediator;

namespace FilesStorage.App.Features;

public class DownloadQuery : IQuery<DownloadResult>
{

    public required Guid FileId { get; init; }

}