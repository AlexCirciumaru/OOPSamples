using System;
using System.IO;
using System.Reflection;
using CustomerAppPaymentP.Abstractions;
using CustomerAppPaymentP.Common;
using CustomerAppPaymentP.Repository;

namespace CustomerAppPaymentP.MainApp
{
    class Program
    {
        static void Main(string[] args)
        {
            DataRepository repository = new DataRepository();
            repository.Initialize();

            ConsoleMenuController menuController = new ConsoleMenuController(repository);
            menuController.Initialize();
            GenericPluginManager<IPaymentProcessorPlugin> pluginManager = new GenericPluginManager<IPaymentProcessorPlugin>();
            try
            {
                pluginManager.LoadPlugins();       
                foreach(var plugin in pluginManager.Plugins)
                {
                    menuController.AddAvailablePaymentProcessor(plugin);
                }
            }
            catch(DirectoryNotFoundException e)
            {
                Console.WriteLine($"WARNING: The plugins directory: {e.Message} does not exists. Press any key to continue ...");
                Console.ReadLine();
            }
            catch(ReflectionTypeLoadException e)
            {
                Console.WriteLine("TypeLoad error " + e.Message);
                return;
            }
            catch(Exception e)
            {
            
                Console.WriteLine("Unexpected error occured while loading the plugins. " + e.Message);
                return;
            }

            menuController.EnterMainMenu();
        }
    }
}
