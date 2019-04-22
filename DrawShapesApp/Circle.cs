using System;

namespace DrawShapesApp
{
    public class Circle : IShape
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Point CenterPoint { get; set; }
        public double Radius { get; set; }

        public void ReadCoordinates()
        {
            var inputRadius = "";
            var result = 0.0;
            do
            {
                Console.Write("Enter the radius of the circle : ");
                inputRadius = Console.ReadLine();
            } while (!Double.TryParse(inputRadius, out result) || result <= 0);
            Radius = result;
        }

        public void Draw()
        {
            ConsoleColor borderColor = ConsoleColor.Blue;
            Console.ForegroundColor = borderColor;
            char symbol = '*';
            double thickness = 0.4;
            ReadCoordinates();

            double rIn = Radius - thickness, rOut = Radius + thickness;

            for (double y = Radius; y >= -Radius; --y)
            {
                for (double x = -Radius; x < rOut; x += 0.5)
                {
                    double value = x * x + y * y;
                    if (value >= rIn * rIn && value <= rOut * rOut)
                    {
                        Console.Write(symbol);
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }

        public string GetName()
        {
            return Name;
        }

        public int GetId()
        {
            return Id;
        }

        public void GetShapeDetails(){
            Console.WriteLine(" with radius of " + Radius);
        }
    }
}