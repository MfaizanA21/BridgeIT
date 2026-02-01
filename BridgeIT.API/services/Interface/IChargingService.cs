namespace BridgeIT.API.services.Interface;

public interface IChargingService
{
    public Task<string> CreateUserConnectAccountAsync(string email, string firstName, string lastName);
    Task<string> CreateCheckoutSessionAsync(int amount, string successUrl, string cancelUrl, Dictionary<string, string> metadata, string accountId);

}