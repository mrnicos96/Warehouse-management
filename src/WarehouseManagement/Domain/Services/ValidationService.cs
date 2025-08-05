using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.ValueObjects;

namespace Domain.Services
{
    public class ValidationService
    {
        private readonly IResourceRepository _resourceRepo;
        private readonly IUnitRepository _unitRepo;
        private readonly IReceiptDocumentRepository _receiptRepo;
        private readonly IShipmentDocumentRepository _shipmentRepo;

        public ValidationService(
            IResourceRepository resourceRepo,
            IUnitRepository unitRepo,
            IReceiptDocumentRepository receiptRepo,
            IShipmentDocumentRepository shipmentRepo)
        {
            _resourceRepo = resourceRepo;
            _unitRepo = unitRepo;
            _receiptRepo = receiptRepo;
            _shipmentRepo = shipmentRepo;
        }

        public async Task ValidateDocumentNumberUniquenessAsync(
            DocumentNumber number,
            DocumentType type)
        {
            bool exists = type switch
            {
                DocumentType.Receipt => await _receiptRepo.ExistsByNumberAsync(number),
                DocumentType.Shipment => await _shipmentRepo.ExistsByNumberAsync(number),
                _ => throw new ArgumentOutOfRangeException()
            };

            if (exists)
                throw new DuplicateDocumentNumberException(number);
        }

        public async Task ValidateResourceUsageAsync(Guid resourceId)
        {
            var isUsed = await _resourceRepo.IsResourceUsedAsync(resourceId);
            if (isUsed)
                throw new ResourceInUseException(resourceId);
        }
    }
}
