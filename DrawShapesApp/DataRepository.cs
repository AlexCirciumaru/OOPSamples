using System;
using System.Collections.Generic;

namespace DrawShapesApp
{
    public class DataRepository
    {
        public List<IShape> Shapes { get; private set; }

        public void Initialize()
        {
            Shapes = new List<IShape>
            {
                new Circle {Id = 1, Name = "Circle"},
                new Square {Id = 2, Name = "Square"},
            };
        }
    }
}