using Domain.Exceptions;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Balance : Entity
    {
        public CompositeKey Key { get; private set; }  // Объединение ResourceId + UnitId
        public Quantity Quantity { get; private set; } 

        public Balance(CompositeKey key, Quantity quantity)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            Quantity = quantity ?? throw new ArgumentNullException(nameof(quantity));
        }

        public void Increase(Quantity amount)
            => Quantity = Quantity.Add(amount);

        public void Decrease(Quantity amount)
        {
            if (Quantity.Value < amount.Value)
                throw new InsufficientBalanceException();

            Quantity = Quantity.Subtract(amount);
        }
    }
}
