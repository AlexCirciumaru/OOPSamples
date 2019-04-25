using System;
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
            menuController.EnterMainMenu();
        }
    }
}
