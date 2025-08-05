namespace Domain.Services
{
    public interface IArchivalService
    {
        Task ArchiveResourceAsync(Guid resourceId);
        Task ArchiveUnitAsync(Guid unitId);
        Task ArchiveClientAsync(Guid clientId);
        Task<bool> CanArchiveResourceAsync(Guid resourceId);
        Task<bool> CanArchiveUnitAsync(Guid unitId);
        Task<bool> CanArchiveClientAsync(Guid clientId);
    }
}
