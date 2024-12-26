using Stripe;
namespace BridgeITAPIs.Helper;

public class PaymentService
{ 
    public async Task<string> CreateStudentAccount(string email, string firstname, string lastname)
    {
        var options = new AccountCreateOptions
        {
            TosAcceptance = new AccountTosAcceptanceOptions
            {
                Date = DateTime.UtcNow,
                Ip = "192.168.1.1" // Replace with actual IP address in production
            },
            Individual = new AccountIndividualOptions
            {
                FirstName = firstname,
                LastName = lastname,
                Email = email,
                Phone = "8888675309",
                Dob = new DobOptions
                {
                    Day = 1,
                    Month = 1,
                    Year = 2000
                },
                Address = new AddressOptions
                {
                    City = "New York",
                    Line1 = "Home",
                    PostalCode = "10001",
                    State = "New York"
                }
            },
            BusinessProfile = new AccountBusinessProfileOptions
            {
                Url = $"https://{email.Split('@')[0]}.com",
            },
            BusinessType = "individual",
            ExternalAccount = new AccountBankAccountOptions
            {
                Country = "US",
                Currency = "usd",
                AccountHolderName = $"{firstname} {lastname}",
                AccountHolderType = "individual",
                RoutingNumber = "110000000", // Replace with actual bank details
                AccountNumber = "000123456789"
            },
            Country = "US",
            Email = email,
            Capabilities = new AccountCapabilitiesOptions
            {
                Transfers = new AccountCapabilitiesTransfersOptions { Requested = true }
            },
            Controller = new AccountControllerOptions
            {
                Losses = new AccountControllerLossesOptions{ Payments = "application"},
                Fees = new AccountControllerFeesOptions{ Payer = "application"},
                StripeDashboard = new AccountControllerStripeDashboardOptions {Type = "none"},
                RequirementCollection = "application"
            }
        };

        var accountService = new AccountService();
        var account = await accountService.CreateAsync(options);

        return account.Id;
    }
    
    public async Task<PaymentIntent> CreatePaymentIntentAsync(double amount, string studentPaymentAccountId, string projectId)
    {
        var options = new PaymentIntentCreateOptions
        {
            Amount = (long)amount,
            Currency = "pkr",
            PaymentMethodTypes = new List<string> { "card" },
            CaptureMethod = "manual",
            Description = "Project Payment",
            Metadata = new Dictionary<string, string>
            {
                { "appointment_id", projectId } // Track your condition (e.g., appointment ID)
            },
            ApplicationFeeAmount = (long)(amount * 0.1), // 10% platform fee in cents
            TransferData = new PaymentIntentTransferDataOptions
            {
                Destination = studentPaymentAccountId // Connected account ID
            }
        };

        var service = new PaymentIntentService();
        var intent = await service.CreateAsync(options);

        return intent;
    }

}