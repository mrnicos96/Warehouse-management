using Domain.ValueObjects;

namespace Domain.Events
{
    public record ReceiptResourceAddedEvent(
        Guid ReceiptDocumentId,
        Guid ResourceId,
        Guid UnitId,
        Quantity Quantity
    ) : IDomainEvent;
}
