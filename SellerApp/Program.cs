using System;

namespace SellerApp.UI
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
