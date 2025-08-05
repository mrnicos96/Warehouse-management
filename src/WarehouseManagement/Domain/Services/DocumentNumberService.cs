using Domain.Interfaces;

namespace Domain.Services
{
    public class DocumentNumberService
    {
        private readonly IReceiptDocumentRepository _receiptRepo;
        private readonly IShipmentDocumentRepository _shipmentRepo;

        public DocumentNumberService(
            IReceiptDocumentRepository receiptRepo,
            IShipmentDocumentRepository shipmentRepo)
        {
            _receiptRepo = receiptRepo;
            _shipmentRepo = shipmentRepo;
        }

        public async Task<string> GenerateReceiptNumberAsync()
        {
            var prefix = "REC-";
            var lastNumber = await _receiptRepo.GetLastDocumentNumberAsync();
            return $"{prefix}{GetNextNumber(lastNumber)}";
        }

        public async Task<string> GenerateShipmentNumberAsync()
        {
            var prefix = "SHIP-";
            var lastNumber = await _shipmentRepo.GetLastDocumentNumberAsync();
            return $"{prefix}{GetNextNumber(lastNumber)}";
        }

        private int GetNextNumber(string? lastNumber)
        {
            if (string.IsNullOrEmpty(lastNumber))
                return 1;

            var numberPart = lastNumber.Split('-').Last();
            return int.Parse(numberPart) + 1;
        }
    }
}
