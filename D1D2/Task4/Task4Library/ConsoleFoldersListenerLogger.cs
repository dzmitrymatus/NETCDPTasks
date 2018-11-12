using System;

namespace Task4Library
{
    public class ConsoleFoldersListenerLogger : IFolderListenerLogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
