namespace Domain.ValueObjects
{
    public sealed class DateRange : IEquatable<DateRange>
    {
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        public DateRange(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
                throw new ArgumentException("Начальная дата не может быть позже конечной");

            StartDate = startDate.Date;
            EndDate = endDate.Date;
        }

        public bool Includes(DateTime date)
            => date.Date >= StartDate && date.Date <= EndDate;

        public bool Equals(DateRange? other)
            => other != null && StartDate == other.StartDate && EndDate == other.EndDate;

        public override bool Equals(object? obj)
            => obj is DateRange other && Equals(other);

        public override int GetHashCode()
            => HashCode.Combine(StartDate, EndDate);
    }
}
