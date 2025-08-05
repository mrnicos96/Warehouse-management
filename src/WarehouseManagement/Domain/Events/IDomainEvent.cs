using MediatR;

namespace Domain.Events
{
    // Базовый интерфейс события
    public interface IDomainEvent  : INotification{ }
}
