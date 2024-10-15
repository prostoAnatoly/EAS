using Shared.Mediator;

namespace FilesStorage.App.Features;

public sealed class DeleteCommand : ICommand
{
    public required Guid FileId { get; init; }

}