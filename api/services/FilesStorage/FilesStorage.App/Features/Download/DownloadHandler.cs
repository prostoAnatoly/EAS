using FilesStorage.App.Helpers;
using FilesStorage.App.Infrastructure.DbContexts;
using FilesStorage.App.Infrastructure.Services;
using FilesStorage.Domain.Aggregates.Files;
using Microsoft.EntityFrameworkCore;
using Shared.Mediator;

namespace FilesStorage.App.Features;

sealed class DownloadHandlerHandler : IQueryHandler<DownloadQuery, DownloadResult>
{
    private readonly IFilesStorageService filesStorageService;
    private readonly IFilesStoragesContext filesStorageContext;

    public DownloadHandlerHandler(IFilesStorageService filesStorageService, IFilesStoragesContext filesStorageContext)
    {
        this.filesStorageService = filesStorageService;
        this.filesStorageContext = filesStorageContext;
    }

    public async Task<DownloadResult> Handle(DownloadQuery request, CancellationToken cancellationToken)
    {
        var fileId = new FileId(request.FileId);

        var fileName = await filesStorageContext.FileProps
            .Where(x => x.Id == fileId)
            .Select(x => x.FileName)
            .FirstOrDefaultAsync(cancellationToken) ?? throw ExceptionHelper.GetEntityNotFoundFileException(fileId);

        var stream = filesStorageService.GetFile(fileId);

        return new DownloadResult(stream, fileName);
    }
}