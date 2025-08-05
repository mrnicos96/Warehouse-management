using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IClientRepository : IRepository<Client>
    {
        Task<bool> IClientRepository(Guid сlientId, CancellationToken ct = default);
    }
}
