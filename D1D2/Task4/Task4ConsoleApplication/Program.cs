using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Task4Library.Concrete;
using Task4Library.Models;

namespace Task4ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var foldersToListen = new List<string>() { @"E:\Example" };
            var rules = new List<Rule>() { new Rule() { FileNameRegex = ".*", DestinationFolder = @"E:\DestinationFolder" }};
            var defaultDestinationFolder = @"E:\DefaultDestinationFolder";          

            var service = new FilesMoverService(foldersToListen, rules, defaultDestinationFolder);
            service.Start();

            CancellationTokenSource source = new CancellationTokenSource();
            Console.CancelKeyPress += (o, e) =>
            {
                source.Cancel();
                service.Stop();
            };
            Task.Delay(TimeSpan.FromMilliseconds(-1), source.Token).Wait();
        }

        private static void SetUpFolders()
        {

        }
    }
}
