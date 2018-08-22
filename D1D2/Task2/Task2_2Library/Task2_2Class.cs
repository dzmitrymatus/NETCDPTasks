using System;
using System.Linq;

namespace Task2_2Library
{
    public static class Task2_2Class
    {
        public static int ParseStringToInt(string inputString)
        {
            if (string.IsNullOrEmpty(inputString)) throw new ArgumentNullException();           
            bool positiveNumber = inputString[0] != '-';
            if (!positiveNumber)
            {
                inputString = inputString.Substring(1);
            }
            if (inputString.All(ch => char.IsDigit(ch)) == false) throw new FormatException();
            if (inputString.Length > 10) throw new OverflowException();

            uint result = 0;

            foreach (char character in inputString)
            {
                uint number = (uint)character - '0';
                if (result > 0x7FFFFFFF) throw new OverflowException();
                result = (result * 10) + number;
            }

            if (positiveNumber && (int)result < 0) throw new OverflowException();
            if (!positiveNumber && -(int)result > 0) throw new OverflowException();

            return positiveNumber ? (int)result : (int)-result;
        }
    }
}
