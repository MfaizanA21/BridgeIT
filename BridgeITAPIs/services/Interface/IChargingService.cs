namespace BridgeITAPIs.services.Interface;

public interface IChargingService
{
    public Task<string> CreateUserConnectAccountAsync(string email, string firstName, string lastName);
    
    public Task<KeyValuePair<string,string>> CreatePaymentIntentAsync(double amount, string accountId, string projectId);
    
    public Task ReleasePayment(string paymentIntentId);

}