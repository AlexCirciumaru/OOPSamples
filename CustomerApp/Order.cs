using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerApp
{
    public class Order
    {
        private decimal amount = 0.0m;
        private Stock stock = null;
        private readonly List<OrderEntry> orderEntries = new List<OrderEntry>();

        public decimal Amount
        {
            get
            {
                return amount;
            }
            private set
            {
                if (value < 0.0m)
                {
                    throw new ArgumentOutOfRangeException("The value for the amount must be greater than 0.0");
                }

                amount = value;
            }
        }

        public decimal VAT
        {
            get
            {
                return 0.19m * Amount;
            }
        }

        public decimal TotalValue
        {
            get
            {
                return Amount + VAT;
            }
        }

        public Order(Stock stock)
        {
            this.stock = stock;
        }

        public void AddItem(Product p, ulong qty)
        {
            if (p == null)
            {
                throw new ArgumentException("Product is not valid.");
            }
            var currentEntry = orderEntries.Where(entry => entry.ProductId == p.Id)
                            .SingleOrDefault();

            if (currentEntry != null)
            {
                UpdateProductQty(currentEntry.ProductId, currentEntry.Qty + qty);
            }
            else
            {
                OrderEntry orderEntry = new OrderEntry(p.Id, p.Name, p.Price, qty);
                orderEntries.Add(orderEntry);
                Amount += orderEntry.TotalPrice;
            }
        }

        public void UpdateProductQty(int productId, ulong newQty)
        {
            var currentEntry = orderEntries.Where(entry => entry.ProductId == productId)
                            .SingleOrDefault();
            if (currentEntry == null)
            {
                throw new ArgumentException("Product does not exist in the order", "productId");
            }

            Amount -= currentEntry.PricePerUnit * currentEntry.Qty;
            currentEntry.Qty = newQty;
            Amount += currentEntry.Qty * currentEntry.PricePerUnit;

            if (newQty == 0)
            {
                orderEntries.Remove(currentEntry);
            }
        }

        public void RemoveEntryWithIndex(int index)
        {
            var entry = orderEntries.ElementAt(index);
            Amount -= entry.TotalPrice;
            orderEntries.Remove(entry);
        }

        public IEnumerable<OrderEntry> OrderEntries
        {
            get
            {
                return orderEntries.AsReadOnly();
            }
        }
    }
}