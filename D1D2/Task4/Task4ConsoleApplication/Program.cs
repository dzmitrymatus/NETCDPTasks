using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Task4Library;

namespace Task4ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var foldersToListen = new List<string>() { @"E:\Example" };
            var rules = new List<FoldersListenerRule>() { new FoldersListenerRule() { FileNameRegex = ".*", DestinationFolder = @"E:\DestinationFolder" }};
            var logger = new ConsoleFoldersListenerLogger();
            var defaultDestinationFolder = @"E:\DefaultDestinationFolder";

            var listener = new FoldersListener(foldersToListen, rules, logger, defaultDestinationFolder);
            Console.WriteLine("Service is working... ");

            CancellationTokenSource source = new CancellationTokenSource();
            Console.CancelKeyPress += (o, e) =>
            {
                source.Cancel();
            };
            Task.Delay(TimeSpan.FromMilliseconds(-1), source.Token).Wait();
        }
    }
}
