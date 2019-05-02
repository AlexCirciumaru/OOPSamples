using System;
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
            Console.WriteLine("Card payment processing...Successful transaction. ");
        }

        public void ReadDetails()
        {
            CardNumber = DataReaderHelper.ReadStringValue("\nCard Number : ");
            CVS = DataReaderHelper.ReadIntValue("CVS : ");
            ExpirationDate = DataReaderHelper.ReadStringValue("Expiration Date : ");            
        }

        public void SetCallback()
        {
            ReadDetails();
            ProcessPayment();
        }
    }
}