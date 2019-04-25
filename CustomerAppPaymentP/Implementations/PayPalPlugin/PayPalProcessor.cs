using System;

using CustomerAppPaymentP.Abstractions;

namespace CustomerAppPaymentP.Implementations.PayPalPlugin
{
    public class PayPalProcessor : IPaymentProcessor
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public void ProcessPayment()
        {
            Console.WriteLine("PayPal payment processing ...");
        }

        public void SetCallback()
        {
            
        }
    }
}