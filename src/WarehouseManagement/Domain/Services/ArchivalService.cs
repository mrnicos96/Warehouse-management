using Domain.Exceptions;
using Domain.Interfaces;

namespace Domain.Services
{
    public class ArchivalService : IArchivalService
    {
        private readonly IResourceRepository _resourceRepo;
        private readonly IUnitRepository _unitRepo;
        private readonly IClientRepository _clientRepo;
        private readonly IBalanceRepository _balanceRepo;
        private readonly IShipmentDocumentRepository _shipmentRepo;
        private readonly IReceiptDocumentRepository _receiptRepo;

        public ArchivalService(
            IResourceRepository resourceRepo,
            IUnitRepository unitRepo,
            IClientRepository clientRepo,
            IBalanceRepository balanceRepo,
            IShipmentDocumentRepository shipmentRepo,
            IReceiptDocumentRepository receiptRepo)
        {
            _resourceRepo = resourceRepo;
            _unitRepo = unitRepo;
            _clientRepo = clientRepo;
            _balanceRepo = balanceRepo;
            _shipmentRepo = shipmentRepo;
            _receiptRepo = receiptRepo;
        }

        // --- Ресурсы ---
        public async Task ArchiveResourceAsync(Guid resourceId)
        {
            var resource = await _resourceRepo.GetByIdAsync(resourceId);
            if (resource == null)
                throw new EntityNotFoundException("Ресурс не найден");

            if (!await CanArchiveResourceAsync(resourceId))
                throw new InvalidOperationException("Ресурс используется в активных документах");

            resource.Archive();
            await _resourceRepo.UpdateAsync(resource);
        }

        public async Task<bool> CanArchiveResourceAsync(Guid resourceId)
        {
            // Проверка использования в балансах
            bool hasBalance = await _balanceRepo.HasBalanceForResourceAsync(resourceId);
            if (hasBalance) return false;

            // Проверка использования в документах поступления
            bool inReceipts = await _receiptRepo.IsResourceUsedInActiveDocumentsAsync(resourceId);
            if (inReceipts) return false;

            // Проверка использования в документах отгрузки
            bool inShipments = await _shipmentRepo.IsResourceUsedInActiveDocumentsAsync(resourceId);
            return !inShipments;
        }

        // --- Единицы измерения ---
        public async Task ArchiveUnitAsync(Guid unitId)
        {
            var unit = await _unitRepo.GetByIdAsync(unitId);
            if (unit == null)
                throw new EntityNotFoundException("Единица измерения не найдена");

            if (!await CanArchiveUnitAsync(unitId))
                throw new InvalidOperationException("Единица измерения используется в активных документах");

            unit.Archive();
            await _unitRepo.UpdateAsync(unit);
        }

        public async Task<bool> CanArchiveUnitAsync(Guid unitId)
        {
            // Проверка использования в балансах
            bool hasBalance = await _balanceRepo.HasBalanceForUnitAsync(unitId);
            if (hasBalance) return false;

            // Проверка использования в документах
            bool inReceipts = await _receiptRepo.IsUnitUsedInActiveDocumentsAsync(unitId);
            bool inShipments = await _shipmentRepo.IsUnitUsedInActiveDocumentsAsync(unitId);
            return !inReceipts && !inShipments;
        }

        // --- Клиенты ---
        public async Task ArchiveClientAsync(Guid clientId)
        {
            var client = await _clientRepo.GetByIdAsync(clientId);
            if (client == null)
                throw new EntityNotFoundException("Клиент не найден");

            if (!await CanArchiveClientAsync(clientId))
                throw new InvalidOperationException("Клиент используется в активных отгрузках");

            client.Archive();
            await _clientRepo.UpdateAsync(client);
        }

        public async Task<bool> CanArchiveClientAsync(Guid clientId)
        {
            // Клиент используется только в документах отгрузки
            return !await _shipmentRepo.IsClientUsedInActiveDocumentsAsync(clientId);
        }
    }
}
