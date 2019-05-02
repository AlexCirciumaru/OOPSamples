using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerAppPaymentP.Models
{
    public class Customer
    {   
        private Order currentOrder = null;
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public decimal TotalIncome
        {
            get;
            private set;
        }

        private void VerifyCurrentOrderExists()
        {
            if (currentOrder == null)
            {
                throw new InvalidOperationException("Order not started");
            }
        }
        
        public void StartNewOrder(Stock currentStock)
        {
            if (currentOrder != null)
            {
                throw new InvalidOperationException("Cannot start a new order while another is still in progress");
            }     
            currentOrder = new Order(currentStock);
        }

        public void AddItemToOrder(Product productToAdd, ulong qty)
        {            
            VerifyCurrentOrderExists();
            currentOrder.AddItem(productToAdd, qty);                          

        }

        public void RemoveEntryFromOrder(int entryIndex)
        {
            VerifyCurrentOrderExists();
            
            try
            {
               currentOrder.RemoveEntryWithIndex(entryIndex);     
            }
            catch(IndexOutOfRangeException ex)
            {
                   throw new InvalidOperationException("This entry does not exist in the order.", ex);

            }              
        }

        public void RemoveItemFromOrder(int productId, ulong qtyToRemove)
        {
            VerifyCurrentOrderExists();
            var currentEntry = currentOrder.OrderEntries.Where(entry => entry.ProductId == productId)
                                                        .SingleOrDefault();
            if (currentEntry == null)
            {
                throw new ArgumentException("The given product is not in the order");   
            }

            if (qtyToRemove > currentEntry.Qty)
            {
                throw new ArgumentException("Too many products to remove.", "qtyToRemove");
            }

            currentOrder.UpdateProductQty(productId, currentEntry.Qty - qtyToRemove);
        }

        public IEnumerable<OrderEntry> ListOrderEntries()
        {
          return currentOrder.OrderEntries;            
        }

        public OrderSummary GetOrderSummary()
        {
            return new OrderSummary{ Price = currentOrder.Amount, VAT = currentOrder.VAT, TotalValue = currentOrder.TotalValue };
        }

        public void FinalizeOrder(Stock currentStock)
        {
            VerifyCurrentOrderExists();
            var orderEntries = currentOrder.OrderEntries;
            
            foreach(var orderEntry in orderEntries)
            {
                currentStock.GetFromStock(orderEntry.ProductId, orderEntry.Qty);                
            }

            TotalIncome += currentOrder.TotalValue;
            currentOrder = null;
        }

        public void CancelOrder()
        {
            currentOrder = null;
        }
    }
}

