using CustomerAppPaymentP.Common;

namespace CustomerAppPaymentP.Implementations.CardPlugin
{
    public class CardPlugin : GenericPlugin<CardProcessor>
    {
        public CardPlugin() : base("MasterCard")
        {
        }
    }
}