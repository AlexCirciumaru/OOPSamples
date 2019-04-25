using System;
using Lesson2.Abstractions;
using Lesson2;

namespace Lesson2.Implementations.CirclePlugin
{
    public class Circle : IDrawableShape, IReadableShape
    {
        private double Ox = 0.0;
        private double Oy = 0.0;

        private double radius = 0.0;

        public void Draw()
        {
            Console.WriteLine($"Circle. Center: {Ox},{Oy}; Radius {radius}");
        }

        public void Read()
        {
            Ox = DataReaderHelper.ReadDoubleValue("\nCircle Center X: ");
            Oy = DataReaderHelper.ReadDoubleValue("Circle Center Y: ");
            radius = DataReaderHelper.ReadDoubleValue("Circle Radius: ");
        }
    }
}