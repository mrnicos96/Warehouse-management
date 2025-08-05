using Domain.ValueObjects;

namespace Domain.Exceptions
{
    public class DuplicateDocumentNumberException : DomainException
    {
        public DuplicateDocumentNumberException(DocumentNumber number)
            : base($"Документ  '{number}' уже был добавлен.") { }

    }
}
