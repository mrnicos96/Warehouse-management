using Domain.ValueObjects;

namespace Domain.Entities
{
    public class ReceiptResource : Entity
    {
        public Guid ResourceId { get; private set; }
        public Guid UnitId { get; private set; }
        public Quantity Quantity { get; private set; }  // Замена decimal на Quantity

        public ReceiptResource(Guid resourceId, Guid unitId, Quantity quantity)
        {
            ResourceId = resourceId;
            UnitId = unitId;
            Quantity = quantity ?? throw new ArgumentNullException(nameof(quantity));
        }
    }
}
