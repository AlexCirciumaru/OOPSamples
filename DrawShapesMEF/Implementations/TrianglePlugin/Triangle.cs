using System;
using Lesson2.Abstractions;
using Lesson2;

namespace Lesson2.Implementations.TrianglePlugin
{
    public class Triangle : IDrawableShape, IReadableShape
    {
        private double pointOneX = 0.0;
        private double pointOneY = 0.0;
       
        private double pointTwoX = 0.0;
        private double pointTwoY = 0.0;
       
       private double pointThreeX = 0.0;
       private double pointThreeY = 0.0;
       

        public void Draw()
        {
            Console.WriteLine($"Triangle. One: ({pointOneX},{pointOneY}); Two: ({pointTwoX},{pointTwoY}); Three: ({pointThreeX},{pointThreeY}) ");
        }

        public void Read()
        {
           pointOneX = DataReaderHelper.ReadDoubleValue("\nOne X: ");
           pointOneY = DataReaderHelper.ReadDoubleValue("One Y: ");
           pointTwoX = DataReaderHelper.ReadDoubleValue("Two X: ");
           pointTwoY = DataReaderHelper.ReadDoubleValue("Two Y: ");
           pointThreeX = DataReaderHelper.ReadDoubleValue("Three X: ");
           pointThreeY = DataReaderHelper.ReadDoubleValue("Three Y: ");
        }
    }
}