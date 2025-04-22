using BridgeITAPIs.services.Interface;
using Stripe;
using Stripe.Checkout;

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
    
    public async Task<string> CreateCheckoutSessionAsync(int amount, string successUrl, string cancelUrl, string projectId, string accountId)
    {
       amount *= 100; // Convert to PKR paisa

       var options = new SessionCreateOptions
       {
          PaymentMethodTypes = new List<string> { "card" },
          Mode = "payment",
          SuccessUrl = successUrl + "?session_id={CHECKOUT_SESSION_ID}",
          CancelUrl = cancelUrl,
          LineItems = new List<SessionLineItemOptions>
          {
             new SessionLineItemOptions
             {
                PriceData = new SessionLineItemPriceDataOptions
                {
                   Currency = "pkr",
                   UnitAmount = amount,
                   ProductData = new SessionLineItemPriceDataProductDataOptions
                   {
                      Name = "Project Payment",
                      Description = $"Payment for project {projectId}",
                   }
                },
                Quantity = 1
             }
          },
          PaymentIntentData = new SessionPaymentIntentDataOptions
          {
            TransferData = new SessionPaymentIntentDataTransferDataOptions
            {
               Destination = $"{accountId}",
            },
            ApplicationFeeAmount = amount / 15,
          },
          Metadata = new Dictionary<string, string>
          {
             { "project_id", projectId }
          }
       };

       var service = new SessionService();
       var session = await service.CreateAsync(options);

       return session.Url; // This is the redirect URL
    }
}