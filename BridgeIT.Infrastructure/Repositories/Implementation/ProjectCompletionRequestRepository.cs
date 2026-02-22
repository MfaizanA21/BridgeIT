using Microsoft.EntityFrameworkCore;
using BridgeIT.Domain.Enums;
using BridgeIT.Domain.Models;
using BridgeIT.Infrastructure.Persistance;
using BridgeIT.Application.Interfaces.Repositories;

namespace BridgeIT.Infrastructure.Repositories.Implementation;

public class ProjectCompletionRequestRepository : IProjectCompletionRequestsRepository
{
    private readonly BridgeItContext _dbContext;

    public ProjectCompletionRequestRepository(BridgeItContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<RequestForProjectCompletion> CreateAsync(Guid projectId)
    {
        var request = new RequestForProjectCompletion
        {
            Id = Guid.NewGuid(),
            ProjectId = projectId,
            RequestStatus = ProjectRequestStatus.PENDING.ToString()
        };

        await _dbContext.RequestForProjectCompletions.AddAsync(request);
        await _dbContext.SaveChangesAsync();

        return request;
    }

    public async Task<List<RequestForProjectCompletion>> GetByUserIdAsync(Guid userRelatedId)
    {
        return await _dbContext.RequestForProjectCompletions
            .Include(r => r.Project)
                .ThenInclude(p => p.Student)
                    .ThenInclude(s => s.User)
            .Include(r => r.Project)
                .ThenInclude(p => p.IndExpert)
                    .ThenInclude(i => i.User)
            .Where(r => r.Project.StudentId == userRelatedId || r.Project.IndExpertId == userRelatedId)
            .ToListAsync();
    }

    public async Task<RequestForProjectCompletion?> GetByProjectIdAsync(Guid projectId)
    {
        return await _dbContext.RequestForProjectCompletions
            .Include(r => r.Project)
                .ThenInclude(p => p.Student)
                    .ThenInclude(s => s.User)
            .Include(r => r.Project)
                .ThenInclude(p => p.IndExpert)
                    .ThenInclude(i => i.User)
            .FirstOrDefaultAsync(r => r.ProjectId == projectId);
    }

    public async Task<RequestForProjectCompletion?> GetByIdAsync(Guid requestId)
    {
        return await _dbContext.RequestForProjectCompletions
            .Include(r => r.Project)
                .ThenInclude(p => p.Student)
                    .ThenInclude(s => s.User)
            .FirstOrDefaultAsync(r => r.Id == requestId);
    }

    public async Task UpdateStatusAsync(RequestForProjectCompletion request)
    {
        _dbContext.RequestForProjectCompletions.Update(request);
        await _dbContext.SaveChangesAsync();
    }
}