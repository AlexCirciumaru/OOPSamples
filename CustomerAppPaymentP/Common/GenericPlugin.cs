using CustomerAppPaymentP.Abstractions;

namespace CustomerAppPaymentP.Common
{
    public abstract class GenericPlugin<T> : IPaymentProcessorPlugin where T : IPaymentProcessor, new()
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

        public IPaymentProcessor ReadPaymentProcessor()
        {
            var newPaymentProcessor = new T();
            newPaymentProcessor.ReadDetails();
            return newPaymentProcessor;
        }
    }
}