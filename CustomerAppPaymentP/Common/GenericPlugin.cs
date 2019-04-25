namespace CustomerAppPaymentP.Common
{
    public abstract class GenericPlugin
    {
        private string paymentName = "";

        public GenericPlugin(string paymentName)
        {
            this.paymentName = paymentName;
        }

        public string GetName()
        {
            return paymentName;
        }
    }
}