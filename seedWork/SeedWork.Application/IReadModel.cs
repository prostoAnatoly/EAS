namespace SeedWork.Application;

public interface IReadModel
{
}

public interface IReadModelWithId : IReadModel
{
    Guid Id { get; init; }
}

