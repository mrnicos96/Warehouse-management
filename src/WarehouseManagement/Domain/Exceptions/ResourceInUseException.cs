namespace Domain.Exceptions
{
    public class ResourceInUseException : DomainException
    {
        public Guid EntityId { get; }
        public string EntityType { get; }

        public ResourceInUseException(Guid entityId, string entityType)
            : base($"Сущность {entityType} с ID {entityId} используется в активных документах.")
        {
            EntityId = entityId;
            EntityType = entityType;
        }

        public ResourceInUseException(Guid entityId)
            : base($"Сущность  с ID {entityId} используется в активных документах.") { }
    }
}
