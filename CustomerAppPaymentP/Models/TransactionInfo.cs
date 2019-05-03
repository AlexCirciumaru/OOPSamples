using System;

namespace CustomerAppPaymentP.Models
{
    public class TransactionInfo
    {
        public string CustomerName { get; set; }
        public decimal OrderValue { get; set; }
        public string PaymentProcessorName { get; set; }
    }
}