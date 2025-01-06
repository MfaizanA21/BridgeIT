using BridgeITAPIs.services.Interface;
using Stripe;

namespace BridgeITAPIs.services.Implementation;

public class ChargingService: IChargingService
{
    public async Task<string> CreateUserConnectAccountAsync(string email, string firstName, string lastName)
    {
        
      var options = new AccountCreateOptions
      {
         TosAcceptance = new AccountTosAcceptanceOptions
         {
            Date = DateTime.UtcNow,
            Ip = "192.168.1.20"
         },
         Individual = new AccountIndividualOptions
         {
            SsnLast4 = "0000",
            FirstName = firstName,
            LastName = lastName,
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
               City = "New York City",
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
            AccountHolderName = firstName + ' ' + lastName,
            AccountHolderType = "individual",
            RoutingNumber = "110000000",
            AccountNumber = "000123456789",
         },
         Country = "US", // student's country
         Email = email, 
         Capabilities = new AccountCapabilitiesOptions
         {
            Transfers = new AccountCapabilitiesTransfersOptions { Requested = true },
         },
         Controller = new AccountControllerOptions
         {
            Losses = new AccountControllerLossesOptions { Payments = "application" },
            Fees = new AccountControllerFeesOptions { Payer = "application" },
            StripeDashboard = new AccountControllerStripeDashboardOptions { Type = "none" },
            RequirementCollection = "application",
         }
      };
      var accountService = new AccountService();
      var account = await accountService.CreateAsync(options);
      return account.Id;
      
    }

    public async Task<KeyValuePair<string, string>> CreatePaymentIntentAsync(double amount, string accountId, string projectId)
    {
       amount *= 100; // strips uses lowest form of currency. so to send 100.00 rupees it needs to be 100000 paisa
       var options = new PaymentIntentCreateOptions
       {
          Amount = (long)amount,
          Currency = "pkr",
          PaymentMethodTypes = ["card"],
          CaptureMethod = "manual",
          Description = "Appointment Booking Payment",
          Metadata = new Dictionary<string, string>
          {
             { "project_id", projectId }, // Track your condition (project id)
          },
          ApplicationFeeAmount = (long)(amount * 0.1), // 10% platform fee
          TransferData = new PaymentIntentTransferDataOptions
          {
             Destination = accountId // Connected account ID
          }
       };
       var service = new PaymentIntentService();
       var intent = await service.CreateAsync(options);
       return new KeyValuePair<string, string>(intent.Id, intent.ClientSecret); 
    }
}