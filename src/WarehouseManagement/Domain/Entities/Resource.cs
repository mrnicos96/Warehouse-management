using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Resource : Entity
    {
        public EntityName Name { get; private set; }  // Замена string на EntityName
        public EntityStatus Status { get; private set; } = EntityStatus.Active;

        public Resource(EntityName name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public void Archive() => Status = EntityStatus.Archived;
    }
}
