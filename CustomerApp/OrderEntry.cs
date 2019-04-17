using System;

namespace CustomerApp
{
    public class OrderEntry
    {
        public int ProductId
        {
            get;
            private set;
        }
        public string ProductName
        {
            get;
            private set;
        }

        public decimal PricePerUnit
        {
            get;
            private set;
        }

        public ulong Qty
        {
            get;
            set;
        }

        public decimal TotalPrice
        {
            get
            {
                return Qty * PricePerUnit;
            }
        }

        public OrderEntry(int productId, string productName, decimal price, ulong qty)
        {
            ProductId = productId;
            ProductName = productName;
            PricePerUnit = price;
            Qty = qty;
        }
    }
}