namespace Domain.ValueObjects
{
    public sealed class EntityName : IEquatable<EntityName>
    {
        public string Value { get; }
        private const int MinLength = 3;
        private const int MaxLength = 100;

        public EntityName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Наименование не может быть пустым");

            if (value.Length < MinLength || value.Length > MaxLength)
                throw new ArgumentException($"Длина должна быть от {MinLength} до {MaxLength} символов");

            Value = value.Trim();
        }

        // Реализация равенства (без учета регистра)
        public bool Equals(EntityName? other)
            => other != null && Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);

        public override bool Equals(object? obj)
            => obj is EntityName other && Equals(other);

        public override int GetHashCode()
            => Value.ToLower().GetHashCode();

        public override string ToString() => Value;
    }
}
