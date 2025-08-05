using Domain.Enums;

namespace Domain.Services
{
    public interface IValidationService
    {
        Task ValidateDocumentNumberUniquenessAsync(string number, DocumentType type);
        Task ValidateResourceUsageAsync(Guid resourceId);
    }
}
