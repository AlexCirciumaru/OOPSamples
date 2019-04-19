using System;
using System.Collections.Generic;
using System.Linq;

namespace DrawShapesApp.UI
{
    public class DrawController
    {
        private Menu mainMenu = new Menu();

        private readonly DataRepository repository;

        private void ListAllShapes()
        {
            Console.WriteLine("SHAPES");
            Console.WriteLine("{0,4}|{1,40}", "Id", "Name");
            foreach(var shape in repository.Shapes)
            {
                Console.WriteLine("{0,4}|{1,40}", shape.GetId(), shape.GetName());
            }
        }

        private void HandleViewAllShapes()
        {
            Console.Clear();
            if (repository.Shapes.Count() == 0)
            {
                Console.WriteLine("There are no shapes !!");
            }
            else
            {
                ListAllShapes();
            }

            Console.ReadLine();
        }

        private void HandleDrawShape()
        {
            throw new NotImplementedException();
        }

        private void AttachShape(IShape shape)
        {

        }

        public DrawController(DataRepository repository)
        {
            this.repository = repository;
        }

        public void Initialize()
        {
            mainMenu.SetMenuItem(1, "Choose Shape : ", () => HandleViewAllShapes());
        }

        public void EnterMainMenu()
        {
            mainMenu.EnterMenu();
        }
    }
}