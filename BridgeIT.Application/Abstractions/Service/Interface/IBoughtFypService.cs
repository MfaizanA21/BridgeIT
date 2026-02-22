using BridgeIT.Application.DTOs;

namespace BridgeIT.Application.Abstractions.Service.Interface;

public interface IBoughtFypService
{
    Task<List<string>> GetAllBoughtFypsAsync();
    Task<BoughtFypDetailDto?> GetBoughtFypByIdAsync(Guid id);
    Task<bool> AddAgreementAsync(Guid id, byte[] agreementBytes);
}