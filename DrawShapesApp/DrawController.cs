using System;
using System.Collections.Generic;
using System.Linq;

namespace DrawShapesApp.UI
{
    public class DrawController
    {
        private Menu mainMenu = new Menu();

        private IShape currentShape = null;

        private readonly DataRepository repository;

        private void ListAllShapes()
        {
            Console.WriteLine("SHAPES\n");
            Console.WriteLine("{0,4}|{1,40}", "Id", "Name");
            foreach (var shape in repository.Shapes)
            {
                Console.WriteLine("{0,4}|{1,40}", shape.GetId(), shape.GetName());
            }
        }

        private void ViewAllShapes()
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
        }

        private int ReadShapeId()
        {            
            int shapeId = 0;
            var readId = "";
            do
            {
                Console.Write("Shape Id: ");
                readId = Console.ReadLine();

            } while (!Int32.TryParse(readId, out shapeId));

            return shapeId;
        }

        private IShape GetShapeToDraw()
        {
            Console.WriteLine("Choose a Shape to draw : ");
            int shapeId = 0;
            shapeId = ReadShapeId();            
            var shapeToDraw = repository.Shapes.Where(entry => entry.GetId() == shapeId).SingleOrDefault();
            if (shapeToDraw == null)
            {
                throw new ShapeNotFoundException();
            }         
            return shapeToDraw;
        }

        private void HandleDrawShape()
        {    
            ViewAllShapes();        
            do
            {
                try
                {
                    currentShape = GetShapeToDraw();
                    currentShape.Draw();
                } 
                catch (ShapeNotFoundException)
                {
                    Console.WriteLine("This id is not associated to an available shape.");
                }
            } while (currentShape == null); 
            Console.ReadLine();           
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
            mainMenu.SetMenuItem(1, "Choose a Shape to Draw: ", () => HandleDrawShape());            
        }

        public void EnterMainMenu()
        {
            mainMenu.EnterMenu();
        }
    }
}