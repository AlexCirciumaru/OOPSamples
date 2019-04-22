using System;

namespace DrawShapesApp
{
    public class Point
    {
        public decimal XCoordinate { get; set; }
        public decimal YCoordinate { get; set; }
        public char Value { get; set; }

        public Point(decimal xCoordinate, decimal yCoordinate)
        {
            this.XCoordinate = xCoordinate;
            this.YCoordinate = yCoordinate;
        }
    }
}