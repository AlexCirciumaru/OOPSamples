using System;

namespace CustomerAppPaymentP.Common
{
    public class TransactionInfo
    {
        public string CustomerName { get; set; }
        public double OrderValue { get; set; }
        public string PaymentProcessorName { get; set; }
    }
}