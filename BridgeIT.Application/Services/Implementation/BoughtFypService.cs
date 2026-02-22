using BridgeIT.Application.Abstractions.Service.Interface;
using BridgeIT.Application.DTOs;
using BridgeIT.Application.Repositories.Interface;

namespace BridgeIT.Application.Services;

public class BoughtFypService : IBoughtFypService
{
    private readonly IBoughtFypRepository _repository;

    public BoughtFypService(IBoughtFypRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<string>> GetAllBoughtFypsAsync()
    {
        var ids = await _repository.GetAllIdsAsync();
        return ids.Select(id => id.ToString()).ToList();
    }

    public async Task<BoughtFypDetailDto?> GetBoughtFypByIdAsync(Guid id)
    {
        var boughtFyp = await _repository.GetByIdOrRelatedWithDetailsAsync(id);
        if (boughtFyp == null) return null;

        return new BoughtFypDetailDto
        {
            Id = boughtFyp.Id,
            FypId = boughtFyp.FypId,
            IndExpertId = boughtFyp.IndExpertId,
            Price = boughtFyp.Price,
            FypTitle = boughtFyp.Fyp?.Title ?? string.Empty,
            IndExpertName = boughtFyp.IndExpert?.User != null
                ? $"{boughtFyp.IndExpert.User.FirstName} {boughtFyp.IndExpert.User.LastName}"
                : string.Empty
        };
    }

    public async Task<bool> AddAgreementAsync(Guid id, byte[] agreementBytes)
    {
        var boughtFyp = await _repository.GetByIdForUpdateAsync(id);
        if (boughtFyp == null) return false;

        boughtFyp.Agreement = agreementBytes;
        await _repository.SaveChangesAsync();
        return true;
    }
}