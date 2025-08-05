namespace Domain.Exceptions
{
    public class DuplicateEntityException : DomainException
    {
        public DuplicateEntityException(string entityName, string field, object value)
            : base($"Сущность '{entityName}' с значением '{field}={value}' уже существует.") { }
    }
}
