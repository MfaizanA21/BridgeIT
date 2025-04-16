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
    
    
    
    
    // public async Task<KeyValuePair<string, string>> CreatePaymentIntentAsync(double amount, string accountId, string projectId)
    // {
    //    amount *= 100; // Convert to paisa (smallest unit)
    //
    //    if (amount < 10000) 
    //       throw new ArgumentException("Amount must be at least 100 PKR."); // Ensure valid Stripe amount
    //
    //    var options = new PaymentIntentCreateOptions
    //    {
    //       Amount = (long)amount,
    //       Currency = "pkr",
    //       PaymentMethodTypes = new List<string> { "card" },
    //       CaptureMethod = "manual",
    //       Description = "Appointment Booking Payment",
    //       Metadata = new Dictionary<string, string>
    //       {
    //          { "project_id", projectId } 
    //       },
    //       ApplicationFeeAmount = (long)(amount * 0.1), // 10% platform fee
    //       TransferData = new PaymentIntentTransferDataOptions
    //       {
    //          Destination = accountId 
    //       }
    //    };
    //
    //    var service = new PaymentIntentService();
    //    var intent = await service.CreateAsync(options);
    //
    //    return new KeyValuePair<string, string>(intent.Id, intent.ClientSecret);
    // }
    //
    // public async Task ReleasePayment(string paymentIntentId)
    // {
    //    var paymentIntentService = new PaymentIntentService();
    //    await paymentIntentService.CaptureAsync(paymentIntentId); 
    // }
    //
    // public async Task<string> GetPaymentIntentStatusAsync(string paymentIntentId)
    // {
    //    var service = new PaymentIntentService();
    //    var intent = await service.GetAsync(paymentIntentId);
    //    return intent.Status;
    // }
    //
    // public async Task<(string PaymentIntentId, string PaymentClientSecret)?> GetPaymentIntentDetailsAsync(string paymentIntentId)
    // {
    //     try
    //     {
    //         var service = new PaymentIntentService();
    //         var paymentIntent = await service.GetAsync(paymentIntentId);
    //
    //         if (paymentIntent == null)
    //         {
    //             return null;
    //         }
    //
    //         return (paymentIntent.Id, paymentIntent.ClientSecret);
    //     }
    //     catch (StripeException ex)
    //     {
    //         return null; // Handle error gracefully
    //     }
    // }

}