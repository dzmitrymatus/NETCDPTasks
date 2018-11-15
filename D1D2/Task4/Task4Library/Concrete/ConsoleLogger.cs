using System;
using Task4Library.Interfaces;

namespace Task4Library.Concrete
{
    public class ConsoleLogger : ILogger
    {
        #region Methods
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
        #endregion
    }
}
