using System;
using System.Collections.Generic;
using CustomerAppPaymentP.Models;

namespace CustomerAppPaymentP.Repository
{
    public class DataRepository
    {
        public List<Customer> Customers
        {
            get;
            private set;
        }

        public List<Product> Products
        {
            get;
            private set;
        }

        public Stock ProductsStock
        {
            get;
            private set;
        }

        public void Initialize()
        {
            Customers = new List<Customer>
            {
                new Customer {Id = 1, Name = "Customer 1", PhoneNumber = "1234" },
                new Customer {Id = 2, Name = "Customer 2", PhoneNumber = "12345" },
                new Customer {Id = 3, Name = "Customer 3", PhoneNumber = "02929838323244" },
                new Customer {Id = 4, Name = "Customer 4", PhoneNumber = "02929838323234" },
                new Customer {Id = 5, Name = "Customer 5", PhoneNumber = "02929838323343" },
                new Customer {Id = 6, Name = "Customer 6", PhoneNumber = "02929833433232" },
                new Customer {Id = 7, Name = "Customer 7", PhoneNumber = "02929834383232" },
            };

            Products = new List<Product>
            {
                new Product {Id = 1, Name = "Product 1", Description = "Product 1 description", Price = 10.0m },
                new Product {Id = 2, Name = "Product 2", Description = "Product 2 description", Price = 10.20m },
                new Product {Id = 3, Name = "Product 3", Description = "Product 3 description", Price = 1.20m },
                new Product {Id = 4, Name = "Product 4", Description = "Product 4 description", Price = 5.2m },
                new Product {Id = 5, Name = "Product 5", Description = "Product 5 description", Price = 3.2m },
                new Product {Id = 6, Name = "Product 6", Description = "Product 6 description", Price = 16.0m }
            };
            Random random = new Random();


            ProductsStock = new Stock();
            foreach (var product in Products)
            {
                ProductsStock.AddToStock(product, (ulong)random.Next());
            }
        }
    }
}