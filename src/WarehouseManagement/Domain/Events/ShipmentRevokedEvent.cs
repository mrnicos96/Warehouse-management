using Domain.ValueObjects;

namespace Domain.Events
{
    public record ShipmentRevokedEvent(
        Guid ShipmentId,
        List<(Guid ResourceId, Guid UnitId, Quantity Quantity)> Items
    ) : IDomainEvent;
}
