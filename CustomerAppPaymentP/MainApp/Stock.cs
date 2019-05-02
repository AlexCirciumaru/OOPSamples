using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerAppPaymentP.Models
{
    public class Stock
    {
        private readonly List<StockEntry> stockEntries = new List<StockEntry>();

        private StockEntry GetStockEntryFromProductId(int productId)
        {
            var stockEntry = stockEntries.Where(entry => entry.Product.Id == productId)
                        .SingleOrDefault();
            return stockEntry;
        }

        public Stock()
        {

        }

        public void AddToStock(Product p, ulong qty)
        {
            var stockEntry = GetStockEntryFromProductId(p.Id);
            if (stockEntry != null)
            {
                stockEntry.AddQty(qty);
            }
            else
            {
                stockEntry = new StockEntry(p, qty);
                stockEntries.Add(stockEntry);
            }
        }

        public void GetFromStock(int productId, ulong qty)
        {
            var stockEntry = GetStockEntryFromProductId(productId);
            if (stockEntry == null)
            {
                throw new ArgumentException("Product does not exist in stock", "productId");
            }
            if (stockEntry.Qty < qty)
            {
                throw new ArgumentException("Not enough stock for the product", "qty");
            }

            stockEntry.RemoveQty(qty);
        }

        public bool IsInStock(int productId, ulong qty)
        {
            var stockEntry = GetStockEntryFromProductId(productId);

            if (stockEntry != null && stockEntry.Qty > qty)
                return true;

            return false;
        }

        public IEnumerable<StockEntry> StockEntries
        {
            get
            {
                return stockEntries.AsEnumerable();
            }
        }
    }
}