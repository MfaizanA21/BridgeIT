namespace BridgeITAPIs.services.Interface;

public interface IChargingService
{
    public Task<string> CreateUserConnectAccountAsync(string email, string firstName, string lastName);
    
    public Task<KeyValuePair<string,string>> CreatePaymentIntentAsync(double amount, string accountId, string projectId);

    Task<(string PaymentIntentId, string PaymentClientSecret)?> GetPaymentIntentDetailsAsync(string paymentIntentId);


    public Task ReleasePayment(string paymentIntentId);

    public Task<string> GetPaymentIntentStatusAsync(string paymentIntentId);

}