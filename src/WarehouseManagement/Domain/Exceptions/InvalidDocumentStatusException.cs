namespace Domain.Exceptions
{
    public class InvalidDocumentStatusException : DomainException
    {
        public InvalidDocumentStatusException(string currentStatus, string allowedStatuses)
            : base($"Недопустимый статус документа. Текущий: {currentStatus}. Допустимые: {allowedStatuses}.") { }

        public InvalidDocumentStatusException(string message)
            : base(message) { }
    }
}
