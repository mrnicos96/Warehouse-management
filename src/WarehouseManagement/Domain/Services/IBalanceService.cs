using Domain.ValueObjects;

namespace Domain.Services
{
    public interface IBalanceService
    {
        Task ValidateBalanceForShipmentAsync(IEnumerable<(Guid ResourceId, Guid UnitId, Quantity Quantity)> items);
        Task UpdateOnReceiptAsync(Guid resourceId, Guid unitId, Quantity quantity);
    }
}
