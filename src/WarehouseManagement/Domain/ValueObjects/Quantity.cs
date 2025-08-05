namespace Domain.ValueObjects
{
    public sealed class Quantity : IEquatable<Quantity>
    {
        public decimal Value { get; }

        public Quantity(decimal value)
        {
            if (value <= 0)
                throw new ArgumentException("Количество должно быть положительным");

            Value = value;
        }

        public Quantity Add(Quantity other)
            => new(Value + other.Value);

        public Quantity Subtract(Quantity other)
        {
            if (Value < other.Value)
                throw new InvalidOperationException("Недостаточное количество");

            return new(Value - other.Value);
        }

        // Реализация равенства (пропорционально точности)
        public bool Equals(Quantity? other)
            => other != null && Math.Abs(Value - other.Value) < 0.001m;

        public override bool Equals(object? obj)
            => obj is Quantity other && Equals(other);

        public override int GetHashCode()
            => Value.GetHashCode();

        public static Quantity operator +(Quantity a, Quantity b) => a.Add(b);
        public static Quantity operator -(Quantity a, Quantity b) => a.Subtract(b);
    }
}
