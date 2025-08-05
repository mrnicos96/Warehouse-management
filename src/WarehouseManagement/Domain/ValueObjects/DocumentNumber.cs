using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public sealed class DocumentNumber : IEquatable<DocumentNumber>
    {
        public string Value { get; }
        private const string Pattern = @"^(REC|SHIP)-\d{4}-\d{3}$";

        public DocumentNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Номер документа не может быть пустым");

            if (!Regex.IsMatch(value, Pattern))
                throw new ArgumentException($"Неверный формат номера. Пример: REC-2023-001");

            Value = value;
        }

        // Реализация равенства
        public bool Equals(DocumentNumber? other)
            => other != null && Value == other.Value;

        public override bool Equals(object? obj)
            => obj is DocumentNumber other && Equals(other);

        public override int GetHashCode()
            => Value.GetHashCode();

        public static bool operator ==(DocumentNumber left, DocumentNumber right)
            => Equals(left, right);

        public static bool operator !=(DocumentNumber left, DocumentNumber right)
            => !Equals(left, right);

        public override string ToString() => Value;
    }
}
