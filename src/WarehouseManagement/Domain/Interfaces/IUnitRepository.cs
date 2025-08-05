using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUnitRepository : IRepository<Unit>
    {
        Task<bool> IsUnitUsedAsync(Guid unitId, CancellationToken ct = default);
    }
}
