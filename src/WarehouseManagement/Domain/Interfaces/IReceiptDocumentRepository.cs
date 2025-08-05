using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Interfaces
{
    public interface IReceiptDocumentRepository : IRepository<ReceiptDocument>
    {
        Task<bool> ExistsByNumberAsync(DocumentNumber number, CancellationToken ct = default);
        Task<ReceiptDocument?> GetWithResourcesAsync(Guid id, CancellationToken ct = default);

        Task<string?> GetLastDocumentNumberAsync(CancellationToken ct = default);

        Task<bool> IsReceiptUsedAsync(Guid unitId, CancellationToken ct = default);

        Task<bool> IsResourceUsedInActiveDocumentsAsync(Guid resourceId);
        Task<bool> IsUnitUsedInActiveDocumentsAsync(Guid unitId);
    }
}
