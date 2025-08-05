using Domain.ValueObjects;

namespace Domain.Events
{
    public record ShipmentSignedEvent(
    Guid ShipmentId,
    List<(Guid ResourceId, Guid UnitId, Quantity Quantity)> Items
    ) : IDomainEvent;
}
