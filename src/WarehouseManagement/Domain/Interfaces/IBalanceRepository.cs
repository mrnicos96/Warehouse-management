using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IBalanceRepository : IRepository<Balance>
    {
        Task<Balance?> GetByResourceAndUnitAsync(Guid resourceId, Guid unitId, CancellationToken ct = default);
        Task UpdateRangeAsync(IEnumerable<Balance> balances, CancellationToken ct = default);

        Task<bool> HasBalanceForResourceAsync(Guid resourceId);
        Task<bool> HasBalanceForUnitAsync(Guid unitId);
    }
}
