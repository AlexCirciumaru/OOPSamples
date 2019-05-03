using System;

using CustomerAppPaymentP.Models;
using CustomerAppPaymentP.Abstractions;
using CustomerAppPaymentP.Common;

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

        public void ReadDetails()
        {
            Email = DataReaderHelper.ReadStringValue("\nEmail : ");
            Password = DataReaderHelper.ReadStringValue("Password : ");
        }

        public void SetCallback(Customer customer, Stock stock)
        {
            Console.ReadLine();                        
            try
            {
                customer.FinalizeOrder(stock);
                Console.WriteLine("\n\nSuccessfully finalized the order. Press enter to go back to Main Menu.");
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}