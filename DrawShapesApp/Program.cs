using System;
using DrawShapesApp.UI;

namespace DrawShapesApp
{
    class Program
    {
        static void Main(string[] args)
        {
            DataRepository repository = new DataRepository();
            repository.Initialize();

            DrawController drawController = new DrawController(repository);
            drawController.Initialize();
            drawController.EnterMainMenu();
        }
    }
}
