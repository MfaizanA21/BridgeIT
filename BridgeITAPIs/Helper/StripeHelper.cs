using Stripe;
namespace BridgeITAPIs.Helper;

public class StripeHelper
{
    public static void ConfigureStripe(string apiKey)
    {
        StripeConfiguration.ApiKey = apiKey;
    }
    
}