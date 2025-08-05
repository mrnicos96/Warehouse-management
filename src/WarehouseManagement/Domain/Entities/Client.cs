using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Client : Entity
    {
        public EntityName Name { get; private set; }
        public Address Address { get; private set; }  // Замена string на Address

        public Client(EntityName name, Address address)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }

        public void Archive() => Status = EntityStatus.Archived;
    }
}
