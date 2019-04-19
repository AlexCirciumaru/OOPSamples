using System;

namespace DrawShapesApp
{
    public class Circle : IShape
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Point CenterPoint {get; private set; }
        public decimal Radius {get; private set; }

        public void ReadCoordinates()
        {
            Console.WriteLine("Enter the coordinates of center point : ");
            Console.WriteLine("Enter X coordinate : ");
            
        }

        public void Draw()
        {
            throw new NotImplementedException();
        }

        public string GetName()
        {
            return Name;
        }

        public int GetId(){
            return Id;
        }
    }
}