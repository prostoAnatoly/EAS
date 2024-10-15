using FilesStorage.Domain.Aggregates.Files;
using Shared.App.Exceptions;

namespace FilesStorage.App.Helpers;

static class ExceptionHelper
{

    public static EntityNotFoundException GetEntityNotFoundFileException(FileId fileId)
    {
        return new EntityNotFoundException("Файл отсутствует в системе", fileId);
    }

}
