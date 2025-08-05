using Domain.Enums;
using Domain.Events;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Domain.Services
{
    public class ShipmentSigningService : IShipmentSigningService
    {
        private readonly IShipmentDocumentRepository _shipmentRepo;
        private readonly IBalanceService _balanceService;
        private readonly IValidationService _validationService;
        private readonly IMediator _mediator;

        public ShipmentSigningService(
            IShipmentDocumentRepository shipmentRepo,
            IBalanceService balanceService,
            IValidationService validationService,
            IMediator mediator)
        {
            _shipmentRepo = shipmentRepo;
            _balanceService = balanceService;
            _validationService = validationService;
            _mediator = mediator;
        }

        // --- Подписание документа ---
        public async Task SignShipmentAsync(Guid shipmentId)
        {
            var shipment = await _shipmentRepo.GetWithResourcesAsync(shipmentId);
            if (shipment == null)
                throw new EntityNotFoundException("Документ отгрузки не найден");

            // Проверка статуса (только черновики можно подписать)
            if (shipment.Status != DocumentStatus.Draft)
                throw new InvalidDocumentStatusException(
                    $"Невозможно подписать документ в статусе {shipment.Status}");

            // Проверка наличия ресурсов
            if (!shipment.Resources.Any())
                throw new EmptyShipmentException("Документ отгрузки не содержит ресурсов");

            // Проверка достаточности баланса
            await _balanceService.ValidateBalanceForShipmentAsync(
                shipment.Resources.Select(r =>
                    (r.ResourceId, r.UnitId, r.Quantity)));

            // Подписание документа
            shipment.Sign();

            // Сохранение изменений
            await _shipmentRepo.UpdateAsync(shipment);

            // Публикация события для обновления баланса
            await _mediator.Publish(new ShipmentSignedEvent(
                shipment.Id,
                shipment.Resources.Select(r =>
                    (r.ResourceId, r.UnitId, r.Quantity)).ToList()));
        }

        // --- Отзыв подписи ---
        public async Task RevokeShipmentAsync(Guid shipmentId)
        {
            var shipment = await _shipmentRepo.GetWithResourcesAsync(shipmentId);
            if (shipment == null)
                throw new EntityNotFoundException("Документ отгрузки не найден");

            // Проверка статуса (можно отозвать только подписанные)
            if (shipment.Status != DocumentStatus.Signed)
                throw new InvalidDocumentStatusException(
                    $"Невозможно отозвать документ в статусе {shipment.Status}");

            // Отзыв документа
            shipment.Revoke();

            // Сохранение изменений
            await _shipmentRepo.UpdateAsync(shipment);

            // Публикация события для возврата баланса
            await _mediator.Publish(new ShipmentRevokedEvent(
                shipment.Id,
                shipment.Resources.Select(r =>
                    (r.ResourceId, r.UnitId, r.Quantity)).ToList()));
        }

        // --- Проверка возможности подписания ---
        public async Task<bool> CanSignShipmentAsync(Guid shipmentId)
        {
            var shipment = await _shipmentRepo.GetWithResourcesAsync(shipmentId);
            if (shipment == null || shipment.Status != DocumentStatus.Draft)
                return false;

            try
            {
                await _balanceService.ValidateBalanceForShipmentAsync(
                    shipment.Resources.Select(r =>
                        (r.ResourceId, r.UnitId, r.Quantity)));
                return true;
            }
            catch (InsufficientBalanceException)
            {
                return false;
            }
        }
    }
}
