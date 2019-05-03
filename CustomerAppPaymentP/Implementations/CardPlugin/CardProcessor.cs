using System;
using CustomerAppPaymentP.Models;
using CustomerAppPaymentP.Abstractions;
using CustomerAppPaymentP.Common;

namespace CustomerAppPaymentP.Implementations.CardPlugin
{
    public class CardProcessor : IPaymentProcessor
    {
        public string CardNumber { get; set; }
        public int CVS { get; set; }
        public string ExpirationDate { get; set; }

        public void ProcessPayment()
        {
            Console.WriteLine("\nCard payment processing...Successful transaction. ");
        }

        public void ReadDetails()
        {
            CardNumber = DataReaderHelper.ReadStringValue("\nCard Number : ");
            CVS = DataReaderHelper.ReadIntValue("CVS : ");
            ExpirationDate = DataReaderHelper.ReadStringValue("Expiration Date : ");            
        }

        public void SetCallback(Customer customer,Stock stock)
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