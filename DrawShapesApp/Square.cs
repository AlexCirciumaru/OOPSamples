using System;

namespace DrawShapesApp
{
    public class Square : IShape
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Point Point { get; set; }
        public int Length { get; set; }

        public void ReadCoordinates()
        {
            var inputLength = "";
            var result = 0;
            do
            {
                Console.Write("Enter the Length of the Square : ");
                inputLength = Console.ReadLine();
            } while (!Int32.TryParse(inputLength, out result) || result <= 0);
            Length = result;
        }

        public void Draw()
        {
            ReadCoordinates();
            for (int i = 1; i <= Length; i++)
            {
                //Display columns
                for (int j = 1; j <= Length; j++)
                {
                    if (i == 1 || i == Length)
                    {
                        Console.Write("*");
                    }
                    else if (j == 1 || j == Length)
                    {
                        Console.Write("*");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                //Go to the next line
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

        public void GetShapeDetails()
        {
            Console.WriteLine(" with the length of " + Length);
        }
    }
}