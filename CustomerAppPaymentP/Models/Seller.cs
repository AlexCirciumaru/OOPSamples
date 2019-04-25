using System;
using System.Collections.Generic;

namespace CustomerAppPaymentP.Models
{
    public class Seller
    {
        private Stock currentStock;

        public List<Customer> Customers { get; set; }

        public Seller(Stock stock)
        {
            this.currentStock = stock;
        }

        public void AcceptOrder(Customer customer)
        {

        }

        public void ModifyOrder(Customer customer)
        {

        }

        public void DeclineOrder(Customer customer)
        {
            customer.CancelOrder();
        }
    }
}