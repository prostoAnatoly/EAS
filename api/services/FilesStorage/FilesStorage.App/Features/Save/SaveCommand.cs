using FilesStorage.App.Dtos;
using Shared.Common.IO;
using Shared.Mediator.Generics;

namespace FilesStorage.App.Features;

public sealed class SaveCommand : ICommand<Guid>
{
    public required AsyncFile<FilePropsDto> File { get; init; }

}