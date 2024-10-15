using FilesStorage.App.Infrastructure.DbContexts;
using FilesStorage.App.Infrastructure.Services;
using FilesStorage.Domain.Aggregates.Files;
using Shared.Mediator;

namespace FilesStorage.App.Features;

sealed class SaveHandlerHandler : ICommandHandler<SaveCommand, Guid>
{
    private readonly IFilesStorageService filesStorageService;
    private readonly IFilesStoragesContext filesStorageContext;

    public SaveHandlerHandler(IFilesStoragesContext filesStorageContext, IFilesStorageService filesStorageService)
    {
        this.filesStorageContext = filesStorageContext;
        this.filesStorageService = filesStorageService;
    }

    public async Task<Guid> Handle(SaveCommand request, CancellationToken cancellationToken)
    {
        var fileProps = new FileProps
        {
            FileName = request.File.Props.FileName,
        };

        filesStorageContext.FileProps.Add(fileProps);

        await filesStorageService.Save(fileProps.Id, request.File.Stream);

        return fileProps.Id.Value;
    }
}