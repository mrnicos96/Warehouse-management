namespace Domain.Services
{
    public interface IShipmentSigningService
    {
        Task SignShipmentAsync(Guid shipmentId);
        Task RevokeShipmentAsync(Guid shipmentId);
        Task<bool> CanSignShipmentAsync(Guid shipmentId);
    }
}
