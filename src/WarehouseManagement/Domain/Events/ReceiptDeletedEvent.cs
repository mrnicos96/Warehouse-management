using Domain.ValueObjects;

namespace Domain.Events
{
    public record ReceiptDeletedEvent(
        Guid ReceiptId,
        List<(Guid ResourceId, Guid UnitId, Quantity Quantity)> Items
    ) : IDomainEvent;
}
