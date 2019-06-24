using System;
using Task4Library.Resources.ConsoleLogger;

namespace Task4Library.Logger.Concrete
{
    public class ConsoleLogger : ILogger
    {
        #region Methods
        public void Log(string message)
        {
            Console.WriteLine(ConsoleLoggerResource.LogTemplate, message);
        }
        #endregion
    }
}
