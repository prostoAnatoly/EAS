using MediatR;
using Shared.Domain;

namespace SeedWork.Domain;

public abstract class DomainBaseEvent : IDomainEvent, INotification
{

}
