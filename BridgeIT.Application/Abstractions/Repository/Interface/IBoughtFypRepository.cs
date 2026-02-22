using BridgeIT.Domain.Models;

namespace BridgeIT.Application.Abstractions.Repository.Interface;

public interface IBoughtFypRepository
{
    Task<List<Guid>> GetAllIdsAsync();
    Task<BoughtFyp?> GetByIdOrRelatedWithDetailsAsync(Guid id);
    Task<BoughtFyp?> GetByIdForUpdateAsync(Guid id);
    Task SaveChangesAsync();
}