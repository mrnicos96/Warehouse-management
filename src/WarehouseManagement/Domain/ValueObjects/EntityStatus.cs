namespace Domain.ValueObjects
{
    public sealed class EntityStatus : IEquatable<EntityStatus>
    {
        public static readonly EntityStatus Active = new(nameof(Active));
        public static readonly EntityStatus Archived = new(nameof(Archived));

        private static readonly EntityStatus[] _all = { Active, Archived };

        public string Value { get; }

        private EntityStatus(string value) => Value = value;

        public static EntityStatus FromString(string value)
        {
            var status = _all.FirstOrDefault(s => s.Value == value);
            return status ?? throw new ArgumentException($"Недопустимый статус: {value}");
        }

        public bool Equals(EntityStatus? other)
            => other != null && Value == other.Value;

        public override bool Equals(object? obj)
            => obj is EntityStatus other && Equals(other);

        public override int GetHashCode()
            => Value.GetHashCode();

        public override string ToString() => Value;
    }
}
