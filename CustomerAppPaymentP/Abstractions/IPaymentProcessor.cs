namespace CustomerAppPaymentP.Abstractions
{
    public interface IPaymentProcessor
    {
        void ProcessPayment();

        void SetCallback();

        void ReadDetails();
    }
}