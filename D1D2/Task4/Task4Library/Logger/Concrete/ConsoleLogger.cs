using System;

namespace Task4Library.Logger.Concrete
{
    public class ConsoleLogger : ILogger
    {
        #region Methods
        public void Log(string message)
        {
            Console.WriteLine($"Log: {message}");
        }
        #endregion
    }
}
