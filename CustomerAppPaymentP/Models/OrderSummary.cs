using System;

namespace CustomerAppPaymentP.Models
{
    public class OrderSummary
    {
        public decimal Price { get; set; }
        public decimal VAT { get; set; }
        public decimal TotalValue { get; set; }
    }
}