using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Interfaces
{
    public interface IResourceRepository : IRepository<Resource>
    {
        Task<bool> ExistsByNameAsync(EntityName name, CancellationToken ct = default);
        Task<IEnumerable<Resource>> GetActiveResourcesAsync(CancellationToken ct = default);
        
        Task<bool> IsResourceUsedAsync(Guid unitId, CancellationToken ct = default);

    }
}
