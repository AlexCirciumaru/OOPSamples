using System;
using System.Collections.Generic;
using System.Linq;
using CustomerAppPaymentP.UI;
using CustomerAppPaymentP.Repository;
using CustomerAppPaymentP.Models;
using CustomerAppPaymentP.Common;
using CustomerAppPaymentP.Abstractions;

namespace CustomerAppPaymentP.MainApp
{
    public class ConsoleMenuController
    {
        private Menu clientMenu;
        private Menu loginMenu;
        private Menu paymentMenu;
        private Seller seller;
        private Customer currentCustomer = null;
        private readonly DataRepository repository;
        private List<IPaymentProcessorPlugin> paymentPlugins = new List<IPaymentProcessorPlugin>();
        private IPaymentProcessor currentPaymentProccesor = null;
        private TransactionInfo currentTransactionInfo = null;

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

        private Product GetProductToAdd()
        {
            int productId = 0;
            productId = DataReaderHelper.ReadIntValue("\nProduct Id : ");
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

        private void HandleRemoveProduct()
        {
            Console.Clear();
            ListProductsInOrder();
            int productId = DataReaderHelper.ReadIntValue("\nProduct Id : ");
            ulong qty = DataReaderHelper.ReadLongValue("\nQty (valid number): ");
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
            ulong qty = DataReaderHelper.ReadLongValue("\nQty (valid number): ");
            try
            {
                currentCustomer.AddItemToOrder(productToAdd, qty);
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

        private void HandleCustomerLogin()
        {
            do
            {
                try
                {
                    String phoneNumber = DataReaderHelper.ReadStringValue("\nPhone Number : ");
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

        private void StartNewTransaction()
        {
            paymentMenu.EnterMenu();
            Console.Clear();
            currentPaymentProccesor.ProcessPayment();
            currentPaymentProccesor.SetCallback(currentCustomer, repository.ProductsStock);
            currentTransactionInfo = GetTransactionInfo();
            Console.WriteLine("***********" + currentTransactionInfo.CustomerName + "********" + currentTransactionInfo.OrderValue + "****************" + currentTransactionInfo.PaymentProcessorName);
            Console.ReadLine();
            //HandleFinalizeOrder(); 
            clientMenu.EnterMenu();
        }

        private TransactionInfo GetTransactionInfo()
        {
            return new TransactionInfo { CustomerName = currentCustomer.Name, OrderValue = currentCustomer.TotalIncome, PaymentProcessorName = GetCurrentPaymentPluginName()};
        }

        private string GetCurrentPaymentPluginName()
        {
            string pluginName = "";
            foreach(IPaymentProcessorPlugin plugin in paymentPlugins)
            {
                if(currentPaymentProccesor.GetType().Namespace.Equals(plugin.GetType().Namespace))
                {
                    pluginName = plugin.GetName();
                }
            }
            return pluginName;
        }

        public void AddAvailablePaymentProcessor(IPaymentProcessorPlugin paymentPlugin)
        {
            paymentMenu.SetMenuItem(paymentPlugins.Count + 1, paymentPlugin.GetName(), () => currentPaymentProccesor = paymentPlugin.ReadPaymentProcessor());
            paymentPlugins.Add(paymentPlugin);
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
            paymentMenu = new Menu();
            loginMenu = new Menu();
            clientMenu = new Menu();

            newOrderMenu.SetMenuItem(1, "Add product", () => HandleAddNewProduct());
            newOrderMenu.SetMenuItem(2, "Remove product from order", () => HandleRemoveProduct());
            newOrderMenu.SetMenuItem(3, "Finalize Order", paymentMenu, () => StartNewTransaction());
            newOrderMenu.OnPreRender = new Action( () => ListProductsInOrder());

            clientMenu.OnPreRender = new Action(() => ShowWelcomeMessage());
            clientMenu.SetMenuItem(1, "New Order", newOrderMenu, () => currentCustomer.StartNewOrder(repository.ProductsStock));
            clientMenu.SetMenuItem(2, "View All Products", () => HandleViewAllProducts());
            clientMenu.SetMenuItem(3, "View Products in stock", () => HandleViewStock());
            clientMenu.SetMenuItem(0, "Logout", () => HandleLogout());

            sellerMenu.SetMenuItem(1, "View All Products", () => HandleViewAllProducts());
            sellerMenu.SetMenuItem(2, "View Products in Stock", () => HandleViewStock());
            sellerMenu.SetMenuItem(3, "Logout", () => HandleLogout());

            loginMenu.SetMenuItem(1, "Login as Customer", clientMenu, () => HandleCustomerLogin());
            loginMenu.SetMenuItem(2, "Login as Seller", sellerMenu, () => HandleSellerLogin());       
        }

        public void EnterMainMenu()
        {
            loginMenu.EnterMenu();
        }
    }
}