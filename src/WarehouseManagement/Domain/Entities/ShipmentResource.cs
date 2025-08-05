using Domain.ValueObjects;

namespace Domain.Entities
{
    public class ShipmentResource : Entity
    {
        public Guid ResourceId { get; private set; }
        public Guid UnitId { get; private set; }
        public Quantity Quantity { get; private set; }

        public ShipmentResource(Guid resourceId, Guid unitId, Quantity quantity)
        {
            ResourceId = resourceId;
            UnitId = unitId;
            Quantity = quantity ?? throw new ArgumentNullException(nameof(quantity));
        }
    }
}
