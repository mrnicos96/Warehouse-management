namespace Domain.Exceptions
{
    public class InsufficientBalanceException : DomainException
    {
        public Guid ResourceId { get; }
        public Guid UnitId { get; }
        public decimal Available { get; }
        public decimal Required { get; }

        public InsufficientBalanceException(
            Guid resourceId,
            Guid unitId,
            decimal available,
            decimal required)
            : base($"Недостаточно ресурса {resourceId} (единица: {unitId}). Доступно: {available}, требуется: {required}.")
        {
            ResourceId = resourceId;
            UnitId = unitId;
            Available = available;
            Required = required;
        }

        public InsufficientBalanceException(string message)
            : base(message) { }

        // Упрощенный вариант
        public InsufficientBalanceException()
            : base("Недостаточно ресурсов на складе.") { }
    }
}
