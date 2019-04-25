using System;
using System.Collections.Generic;
using System.Linq;
using CustomerAppPaymentP.UI;
using CustomerAppPaymentP.Repository;
using CustomerAppPaymentP.Models;

namespace CustomerAppPaymentP.MainApp
{
    public class ConsoleMenuController
    {
        private Menu clientMenu;
        private Menu loginMenu = new Menu();
        private Seller seller;
        private Customer currentCustomer = null;
        private readonly DataRepository repository;

        private void ListProductsInStock()
        {
            Console.WriteLine("PRODUCTS ");
            Stock currentStock = repository.ProductsStock;
            Console.WriteLine("{0,4}|{1,60}|{2,10}", "Id", "Name", "Qty");
            foreach (var stockEntry in currentStock.StockEntries)
            {
                Console.WriteLine("{0,4}|{1,60}|{2,10}", stockEntry.Product.Id, stockEntry.Product.Name, stockEntry.Qty);
            }
        }

        private void ListAllProducts()
        {
            Console.WriteLine("PRODUCTS ");
            List<Product> products = repository.Products;
            Console.WriteLine("{0,4}|{1,60}|{2,10}", "Id", "Name", "Price");
            foreach (var product in products)
            {
                Console.WriteLine("{0,4}|{1,60}|{2,10}", product.Id, product.Name, product.Price);
            }
        }

        private void ListProductsInOrder()
        {
            var orderEntries = currentCustomer.ListOrderEntries();
            if (orderEntries.Count() == 0)
            {
                Console.WriteLine("Empty order !!!");
            }
            else
            {
                Console.WriteLine("Order entries ");
                Console.WriteLine("{0,4}|{1,4}|{2,40}|{3,10}|{4,10}|{5, 10}", "#", "Id", "Name", "Qty", "P.P.U", "Total");
                int idx = 0;
                foreach (var orderEntry in orderEntries)
                {
                    Console.WriteLine("{0,4}|{1,4}|{2,40}|{3,10}|{4,10}|{5, 10}",
                                        idx + 1,
                                        orderEntry.ProductId,
                                        orderEntry.ProductName,
                                        orderEntry.Qty,
                                        orderEntry.PricePerUnit,
                                        orderEntry.TotalPrice);
                    idx++;
                }
            }
            var orderSummary = currentCustomer.GetOrderSummary();
            Console.WriteLine("\n\nTOTALS\n--------------------------------------------------------------------------------------------");
            Console.WriteLine("{0, 72} {1, 10}", "Total without VAT:", orderSummary.Price);
            Console.WriteLine("{0, 72} {1, 10}", "VAT:", orderSummary.VAT);
            Console.WriteLine("{0, 72} {1, 10}", "Total with VAT:", orderSummary.TotalValue);
            Console.WriteLine("{0, 72} {1, 10}", "Order created by : ", currentCustomer.Name);

        }

        private int ReadProductId()
        {
            int productId = 0;
            var readId = "";
            do
            {
                Console.Write("Product Id: ");
                readId = Console.ReadLine();

            } while (!Int32.TryParse(readId, out productId));

            return productId;
        }

        private Product GetProductToAdd()
        {
            int productId = 0;
            productId = ReadProductId();
            var currentStock = repository.ProductsStock;
            var stockEntry = currentStock.StockEntries.Where(entry => entry.Product.Id == productId)
                                     .SingleOrDefault();
            Product retVal = null;
            if (stockEntry != null)
            {
                retVal = stockEntry.Product;
            }

            return retVal;
        }

        private ulong ReadQty()
        {
            var readString = "";
            ulong qty = 0;
            do
            {
                Console.Write("Qty (valid number): ");
                readString = Console.ReadLine();

            } while (!ulong.TryParse(readString, out qty));

            return qty;
        }

        private void HandleFinalizeOrder()
        {
            try
            {
                currentCustomer.FinalizeOrder(repository.ProductsStock);
                Console.WriteLine("\n\nSuccessfully finalized the order. Press enter to go back to Main Menu.");
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
            clientMenu.EnterMenu();
        }

        private void HandleRemoveProduct()
        {
            Console.Clear();
            ListProductsInOrder();
            int productId = ReadProductId();
            ulong qty = (ulong)ReadQty();
            try
            {
                currentCustomer.RemoveItemFromOrder(productId, qty);
                Console.WriteLine($"Successfully removed {qty} units of product {productId} from order");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception)
            {
                Console.WriteLine("Unexpected error occured while trying to remove the product from the order.");
            }

            Console.ReadLine();
        }

        private void HandleAddNewProduct()
        {

            Console.Clear();
            ListProductsInStock();
            var productToAdd = GetProductToAdd();
            var qty = ReadQty();
            try
            {
                currentCustomer.AddItemToOrder(productToAdd, (ulong)qty);
                Console.WriteLine($"Product {productToAdd.Name} {qty} pcs added to the order");
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to add product to order. " + e.Message);
            }

            Console.ReadLine();
        }

        private void HandleViewAllProducts()
        {
            Console.Clear();
            if (repository.Products.Count() == 0)
            {
                Console.WriteLine("There are no products !!");
            }
            else
            {
                ListAllProducts();
            }

            Console.ReadLine();
        }

        private void HandleViewStock()
        {
            Console.Clear();
            if (repository.ProductsStock.StockEntries.Count() == 0)
            {
                Console.WriteLine("Stock is empty !!");
            }
            else
            {
                ListProductsInStock();
            }

            Console.ReadLine();
        }

        private string ReadPhoneNumber()
        {
            String phoneNumber = "";
            Console.Write("\nPhone Number : ");
            phoneNumber = Console.ReadLine();
            return phoneNumber;
        }

        private void HandleCustomerLogin()
        {
            do
            {
                try
                {
                    String phoneNumber = ReadPhoneNumber();
                    currentCustomer = FindCurrentCustomer(phoneNumber);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("This phone number isn't assigned to a customer.");
                }
            } while (currentCustomer == null);
        }

        private bool VerifyGivenCustomerPhoneNumberExists(string phoneNumber)
        {
            foreach (Customer c in repository.Customers)
            {
                if (c.PhoneNumber.Equals(phoneNumber))
                {
                    return true;
                }
            }
            throw new ArgumentException("The given phone number does not exist.");
        }

        private Customer FindCurrentCustomer(string phoneNumber)
        {
            Customer currentCustomer = null;
            if (VerifyGivenCustomerPhoneNumberExists(phoneNumber))
            {
                currentCustomer = repository.Customers.Where(customer => customer.PhoneNumber == phoneNumber)
                                            .Single();
            }
            return currentCustomer;

        }
        private void HandleSellerLogin()
        {

        }

        private void HandleLogout()
        {
            currentCustomer = null;
            loginMenu.EnterMenu();
        }

        private void ShowWelcomeMessage()
        {
            if (currentCustomer != null)
            {
                Console.WriteLine($"Welcome, {currentCustomer.Name} !");
            }
            else
            {
                Console.WriteLine("You are not logged.");
            }
        }

        public ConsoleMenuController(DataRepository repository)
        {
            this.repository = repository;
            seller = new Seller(this.repository.ProductsStock);
        }

        public void Initialize()
        {
            Menu newOrderMenu = new Menu();
            Menu sellerMenu = new Menu();
            clientMenu = new Menu();

            newOrderMenu.SetMenuItem(1, "Add product", () => HandleAddNewProduct());
            newOrderMenu.SetMenuItem(2, "Remove product from order", () => HandleRemoveProduct());
            newOrderMenu.SetMenuItem(3, "Finalize Order", () => HandleFinalizeOrder());
            newOrderMenu.OnPreRender = new Action(() => ListProductsInOrder());

            clientMenu.OnPreRender = new Action(() => ShowWelcomeMessage());
            clientMenu.SetMenuItem(1, "New Order", newOrderMenu, () => currentCustomer.StartNewOrder(repository.ProductsStock));
            clientMenu.SetMenuItem(2, "View All Products", () => HandleViewAllProducts());
            clientMenu.SetMenuItem(3, "View Products in stock", () => HandleViewStock());
            clientMenu.SetMenuItem(4, "Logout", () => HandleLogout());

            sellerMenu.SetMenuItem(1, "View All Products", () => HandleViewAllProducts());
            sellerMenu.SetMenuItem(2, "View Products in Stock", () => HandleViewStock());
            sellerMenu.SetMenuItem(3, "Logout", () => HandleLogout());

            loginMenu.SetMenuItem(1, "Login as Customer", clientMenu, () => HandleCustomerLogin());
            loginMenu.SetMenuItem(2, "Login as seller", sellerMenu, () => HandleSellerLogin());
        }
        public void EnterMainMenu()
        {
            loginMenu.EnterMenu();
        }
    }
}