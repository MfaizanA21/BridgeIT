using BridgeIT.Application.Abstractions.Repository.Interface;
using BridgeIT.Domain.Models;
using BridgeIT.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace BridgeIT.Infrastructure.Repositories;

public class BoughtFypRepository : IBoughtFypRepository
{
    private readonly BridgeItContext _dbContext;

    public BoughtFypRepository(BridgeItContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Guid>> GetAllIdsAsync()
    {
        return await _dbContext.BoughtFyps
            .Select(b => b.Id)
            .ToListAsync();
    }

    public async Task<BoughtFyp?> GetByIdOrRelatedWithDetailsAsync(Guid id)
    {
        return await _dbContext.BoughtFyps
            .Include(b => b.Fyp)
            .Include(b => b.IndExpert)
                .ThenInclude(i => i.User)
            .FirstOrDefaultAsync(b => b.Id == id || b.FypId == id || b.IndExpertId == id);
    }

    public async Task<BoughtFyp?> GetByIdForUpdateAsync(Guid id)
    {
        return await _dbContext.BoughtFyps
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}