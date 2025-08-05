using Domain.ValueObjects;

namespace Domain.Events
{
    public record ReceiptCreatedEvent(
        Guid ReceiptId,
        List<(Guid ResourceId, Guid UnitId, Quantity Quantity)> Items
    ) : IDomainEvent;
}
