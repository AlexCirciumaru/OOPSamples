using CustomerAppPaymentP.Common;

namespace CustomerAppPaymentP.Implementations.PayPalPlugin
{
    public class PayPalPlugin : GenericPlugin<PayPalProcessor>
    {
        public PayPalPlugin() : base("PayPal")
        {
        }
    }
}