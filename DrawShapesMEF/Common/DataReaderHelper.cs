using System;
namespace Lesson2
{
    public class DataReaderHelper
    {
        public static double ReadDoubleValue(string infoText)
        {
            Console.WriteLine(infoText);    
            bool validValue = false;
            double  retValue = 0.0;
            
            do
            {
                string readString = Console.ReadLine();
                if (Double.TryParse(readString, out retValue))
                {
                    validValue = true;
                }
                 
            }while(!validValue);

            return retValue;
        } 

        public static int ReadIntValue(string infoText)
        {            
            Console.WriteLine(infoText);    
            bool validValue = false;
            Int32 retValue = 0;            
            do
            {
                string readString = Console.ReadLine();
                if (Int32.TryParse(readString, out retValue))
                {
                    validValue = true;
                }
                 
            }while(!validValue);

            return retValue;

        }

        public static string ReadStringValue(string infoText)
        {
            Console.WriteLine(infoText);            
            bool validValue = false;
            string retValue = "";            
            do
            {
                retValue = Console.ReadLine();
                if (!string.IsNullOrEmpty(retValue))
                {
                    validValue = true;
                }
                 
            }while(!validValue);

            return retValue;

        }
    }

}