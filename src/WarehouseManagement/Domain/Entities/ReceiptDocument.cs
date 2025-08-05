using Domain.Events;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class ReceiptDocument : Entity
    {
        public DocumentNumber Number { get; private set; }  // Замена string на DocumentNumber
        public DateTime Date { get; private set; }
        private readonly List<ReceiptResource> _resources = new();
        public IReadOnlyCollection<ReceiptResource> Resources => _resources.AsReadOnly();

        public ReceiptDocument(DocumentNumber number, DateTime date)
        {
            Number = number ?? throw new ArgumentNullException(nameof(number));
            Date = date;
        }

        public void AddResource(Guid resourceId, Guid unitId, Quantity quantity)
        {
            _resources.Add(new ReceiptResource(resourceId, unitId, quantity));
        }
    }
}
