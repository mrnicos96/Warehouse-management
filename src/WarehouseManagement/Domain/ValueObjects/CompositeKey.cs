namespace Domain.ValueObjects
{
    public sealed class CompositeKey : IEquatable<CompositeKey>
    {
        public Guid ResourceId { get; }
        public Guid UnitId { get; }

        public CompositeKey(Guid resourceId, Guid unitId)
        {
            if (resourceId == Guid.Empty || unitId == Guid.Empty)
                throw new ArgumentException("Идентификаторы не могут быть пустыми");

            ResourceId = resourceId;
            UnitId = unitId;
        }

        public bool Equals(CompositeKey? other)
            => other != null && ResourceId == other.ResourceId && UnitId == other.UnitId;

        public override bool Equals(object? obj)
            => obj is CompositeKey other && Equals(other);

        public override int GetHashCode()
            => HashCode.Combine(ResourceId, UnitId);
    }
}
