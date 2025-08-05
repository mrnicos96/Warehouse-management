using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.ValueObjects;

namespace Domain.Services
{
    public class BalanceService
    {
        private readonly IBalanceRepository _balanceRepo;
        private readonly IResourceRepository _resourceRepo;
        private readonly IUnitRepository _unitRepo;

        public BalanceService(
            IBalanceRepository balanceRepo,
            IResourceRepository resourceRepo,
            IUnitRepository unitRepo)
        {
            _balanceRepo = balanceRepo;
            _resourceRepo = resourceRepo;
            _unitRepo = unitRepo;
        }

        // Обновление баланса при поступлении
        public async Task UpdateOnReceiptAsync(
            Guid resourceId,
            Guid unitId,
            Quantity quantity)
        {
            var resource = await _resourceRepo.GetByIdAsync(resourceId);
            var unit = await _unitRepo.GetByIdAsync(unitId);

            if (resource == null || unit == null)
                throw new InvalidOperationException("Ресурс или единица измерения не найдены");

            if (resource.Status == EntityStatus.Archived || unit.Status == EntityStatus.Archived)
                throw new InvalidOperationException("Нельзя использовать архивные ресурсы или единицы измерения");

            var balance = await _balanceRepo.GetByResourceAndUnitAsync(resourceId, unitId);

            if (balance == null)
            {
                balance = new Balance(new CompositeKey(resourceId, unitId), quantity);
                await _balanceRepo.AddAsync(balance);
            }
            else
            {
                balance.Increase(quantity);
                await _balanceRepo.UpdateAsync(balance);
            }
        }

        // Проверка достаточности баланса для отгрузки
        public async Task ValidateBalanceForShipmentAsync(
            IEnumerable<(Guid resourceId, Guid unitId, Quantity quantity)> items)
        {
            foreach (var item in items)
            {
                var balance = await _balanceRepo.GetByResourceAndUnitAsync(
                    item.resourceId,
                    item.unitId);

                if (balance == null || balance.Quantity.Value < item.quantity.Value)
                    throw new InsufficientBalanceException(
                        $"Недостаточно ресурса {item.resourceId} в единицах {item.unitId}");
            }
        }
    }
}
