using CustomerAppPaymentP.Models;

namespace CustomerAppPaymentP.Abstractions
{
    public interface IPaymentProcessor
    {
        void ProcessPayment();

        void SetCallback(Customer customer, Stock stock);

        void ReadDetails();
    }
}