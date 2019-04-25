using System;
using Lesson2;
using Lesson2.Abstractions;

namespace Lesson2.Implementations.SquarePlugin
{
    public class Square : IDrawableShape, IReadableShape
    {
        private double topRightX = 0.0;
        private double topRightY = 0.0;
        private double length = 0.0;

        public void Draw()
        {
            Console.WriteLine($"Square. Top Right Pos: ({topRightX},{topRightY}); Length : {length}");
        }

        public void Read()
        {
            topRightX = DataReaderHelper.ReadDoubleValue("\nEnter Top Right X: ");
            topRightY = DataReaderHelper.ReadDoubleValue("Enter Top Right Y: ");
            length = DataReaderHelper.ReadDoubleValue("Enter Length: ");
        }
    }
}