using System;

namespace CustomerAppPaymentP.Common
{
    public class DataReaderHelper
    {
        public static double ReadDoubleValue(string infoText)
        {            
            bool validValue = false;
            double retValue = 0.0;

            do
            {
                Console.Write(infoText);
                string readString = Console.ReadLine();
                if (Double.TryParse(readString, out retValue))
                {
                    validValue = true;
                }

            } while (!validValue);

            return retValue;
        }

        public static int ReadIntValue(string infoText)
        {
            bool validValue = false;
            Int32 retValue = 0;
            do
            {
                Console.Write(infoText);
                string readString = Console.ReadLine();
                if (Int32.TryParse(readString, out retValue))
                {
                    validValue = true;
                }

            } while (!validValue);

            return retValue;
        }

        public static ulong ReadLongValue(string infoText)
        {
            bool validValue = false;
            ulong retValue = 0;
            do
            {
                Console.Write(infoText);
                string readString = Console.ReadLine();
                if (ulong.TryParse(readString, out retValue))
                {
                    validValue = true;
                }

            } while (!validValue);

            return retValue;
        }

        public static string ReadStringValue(string infoText)
        {
            bool validValue = false;
            string retValue = "";
            do
            {
                Console.Write(infoText);
                retValue = Console.ReadLine();
                if (!string.IsNullOrEmpty(retValue))
                {
                    validValue = true;
                }

            } while (!validValue);

            return retValue;
        }
    }
}