using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Interfaces
{
    public interface IShipmentDocumentRepository : IRepository<ShipmentDocument>
    {
        Task<IEnumerable<ShipmentDocument>> GetByStatusAsync(DocumentStatus status, CancellationToken ct = default);

        Task<string?> GetLastDocumentNumberAsync(CancellationToken ct = default);

        Task<bool> ExistsByNumberAsync(DocumentNumber number, CancellationToken ct = default);

        Task<bool> IsResourceUsedInActiveDocumentsAsync(Guid resourceId);
        Task<bool> IsUnitUsedInActiveDocumentsAsync(Guid unitId);
        Task<bool> IsClientUsedInActiveDocumentsAsync(Guid clientId);

        Task<ShipmentDocument?> GetWithResourcesAsync(Guid id, CancellationToken ct = default);
    }
}
