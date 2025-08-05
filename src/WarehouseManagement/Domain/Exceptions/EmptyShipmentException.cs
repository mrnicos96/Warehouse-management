namespace Domain.Exceptions
{
    public class EmptyShipmentException : DomainException
    {
        public EmptyShipmentException(Guid shipmentId)
            : base($"Документ отгрузки {shipmentId} не содержит ресурсов.") { }

        public EmptyShipmentException(string message)
            : base(message) { }
    }
}
