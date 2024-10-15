using FilesStorage.App.Helpers;
using FilesStorage.App.Infrastructure.DbContexts;
using FilesStorage.App.Infrastructure.Services;
using FilesStorage.Domain.Aggregates.Files;
using Microsoft.EntityFrameworkCore;
using Shared.Mediator;

namespace FilesStorage.App.Features;

sealed class DeleteHandlerHandler : BaseCommandHandler<DeleteCommand>
{
    private readonly IFilesStorageService filesStorageService;
    private readonly IFilesStoragesContext filesStorageContext;

    public DeleteHandlerHandler(IFilesStoragesContext filesStorageContext, IFilesStorageService filesStorageService)
    {
        this.filesStorageContext = filesStorageContext;
        this.filesStorageService = filesStorageService;
    }

    protected override async Task Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        var fileId = new FileId(request.FileId);

        if (!await filesStorageContext.FileProps.AnyAsync(x => x.Id == fileId, cancellationToken))
        {
            throw ExceptionHelper.GetEntityNotFoundFileException(fileId);
        }

        filesStorageService.Delete(fileId);
    }

}