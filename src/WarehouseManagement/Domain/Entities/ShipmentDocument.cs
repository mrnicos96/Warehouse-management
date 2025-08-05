using Domain.Enums;
using Domain.Events;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class ShipmentDocument : Entity
    {
        private readonly List<ShipmentResource> _resources = new();

        public DocumentNumber Number { get; private set; }
        public Guid ClientId { get; private set; }
        public DateTime Date { get; private set; }
        public DocumentStatus Status { get; private set; } = DocumentStatus.Draft;
        public IReadOnlyCollection<ShipmentResource> Resources => _resources.AsReadOnly();

        public ShipmentDocument(DocumentNumber number, Guid clientId, DateTime date)
        {
            Number = number ?? throw new ArgumentNullException(nameof(number));
            ClientId = clientId;
            Date = date;
        }

        // Подписание документа
        public void Sign()
        {
            if (Status != DocumentStatus.Draft)
                throw new InvalidOperationException("Документ уже подписан или отозван.");

            if (_resources.Count == 0)
                throw new InvalidOperationException("Документ отгрузки не может быть пустым.");

            Status = DocumentStatus.Signed;
            AddDomainEvent(new ShipmentSignedEvent(Id, _resources.Select(r =>
                (r.ResourceId, r.UnitId, r.Quantity)).ToList()));
        }

        public void Revoke()
        {
            if (Status != DocumentStatus.Signed)
                throw new InvalidOperationException("Только подписанные документы можно отозвать");

            Status = DocumentStatus.Revoked;
            AddDomainEvent(new ShipmentRevokedEvent(
                Id,
                Resources.Select(r => (r.ResourceId, r.UnitId, r.Quantity)).ToList()));
        }
    }
}
