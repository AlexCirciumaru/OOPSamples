using System;

namespace DrawShapesApp
{
    public class Square : IShape
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Point Point {get; private set; }
        public decimal Length {get; private set; }

        public void ReadCoordinates()
        {
            throw new NotImplementedException();
        }

        public void Draw()
        {
            throw new NotImplementedException();
        }

        public string GetName()
        {
            return Name;
        }

        public int GetId()
        {
            return Id;
        }
    }
}