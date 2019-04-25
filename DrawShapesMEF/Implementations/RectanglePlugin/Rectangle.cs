using System;
using Lesson2.Abstractions;
using Lesson2;

namespace Lesson2.Implementations.RectanglePlugin
{
    public class Rectangle : IDrawableShape, IReadableShape
    {
        private double topRightX = 0.0;
        private double topRightY = 0.0;
        private double width = 0.0;
        private double height = 0.0;

        public void Draw()
        {
            Console.WriteLine($"Rectangle. Top Right Pos: ({topRightX},{topRightY}); Dimension ({width}, {height})");
        }

        public void Read()
        {
           topRightX = DataReaderHelper.ReadDoubleValue("\nEnter Top Right X: ");
           topRightY = DataReaderHelper.ReadDoubleValue("Enter Top Right Y: ");
           width = DataReaderHelper.ReadDoubleValue("Enter Width: ");
           height = DataReaderHelper.ReadDoubleValue("Enter Height: ");
        }
    }
}