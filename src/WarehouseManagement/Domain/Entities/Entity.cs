using Domain.Events;
using Domain.ValueObjects;

namespace Domain.Entities
{
    // Базовый класс для всех сущностей
    public abstract class Entity
    {
        public Guid Id { get; protected set; }
        public EntityStatus Status { get; protected set; } = EntityStatus.Active;

        private List<IDomainEvent> _domainEvents = new();

        // Возвращает все накопленные события
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        // Добавляет событие в список
        public void AddDomainEvent(IDomainEvent eventItem)
            => _domainEvents.Add(eventItem);

        // Очищает список событий (вызывается после публикации)
        public void ClearDomainEvents()
            => _domainEvents.Clear();
    }
}
