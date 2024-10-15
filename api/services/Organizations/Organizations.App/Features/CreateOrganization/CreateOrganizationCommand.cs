using Shared.Mediator.Generics;

namespace Organizations.App.Features;

public sealed class CreateOrganizationCommand : ICommand<CreateOrganizationResult>
{

    /// <summary>
    /// Наименование организации.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Идентификатор создателя организации.
    /// </summary>
    public required Guid CreatorId { get; init; }
}