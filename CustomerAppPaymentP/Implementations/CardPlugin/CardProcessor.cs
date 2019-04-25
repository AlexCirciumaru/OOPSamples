using System;
using CustomerAppPaymentP.Abstractions;

namespace CustomerAppPaymentP.Implementations.CardPlugin
{
    public class CardProcessor : IPaymentProcessor
    {
        public string CardNumber { get; set; }
        public string CVS { get; set; }
        public string ExpirationDate { get; set; }

        public void ProcessPayment()
        {
            Console.WriteLine("Card payment processing...");
        }

        public void SetCallback()
        {
            
        }
    }
}