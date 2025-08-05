namespace Domain.Exceptions
{
    public class DomainValidationException : DomainException
    {
        public string Field { get; }

        public DomainValidationException(string field, string message)
            : base($"Ошибка валидации поля '{field}': {message}.")
        {
            Field = field;
        }
    }
}
