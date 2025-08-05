namespace Domain.ValueObjects
{
    public sealed class Address : IEquatable<Address>
    {
        public string Value { get; }
        private const int MaxLength = 200;

        public Address(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Адрес не может быть пустым");

            if (value.Length > MaxLength)
                throw new ArgumentException($"Максимальная длина адреса: {MaxLength} символов");

            Value = value.Trim();
        }

        public bool Equals(Address? other)
            => other != null && Value == other.Value;

        public override bool Equals(object? obj)
            => obj is Address other && Equals(other);

        public override int GetHashCode()
            => Value.GetHashCode();
    }
}
