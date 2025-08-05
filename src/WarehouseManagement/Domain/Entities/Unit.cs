using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Unit : Entity
    {
        public EntityName Name { get; private set; }

        public Unit(EntityName name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public void Archive() => Status = EntityStatus.Archived;
    }
}
