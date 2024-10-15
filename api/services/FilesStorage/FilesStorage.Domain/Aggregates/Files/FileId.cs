using Shared.Domain;

namespace FilesStorage.Domain.Aggregates.Files;

public class FileId : StronglyTypedId
{

    public FileId() : base(Guid.NewGuid()) { }

    public FileId(Guid value) : base(value) { }
}
