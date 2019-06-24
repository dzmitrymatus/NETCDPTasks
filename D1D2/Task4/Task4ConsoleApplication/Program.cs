using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Task4Library.Concrete;

namespace Task4ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var foldersToListen = new List<string>() { @"E:\Example" };
            var rules = new List<Rule>() { new Rule() { FileNameRegex = new Regex(@".*"), DestinationFolder = @"E:\DestinationFolder" }};
            var defaultDestinationFolder = @"E:\DefaultDestinationFolder";

            SetUpCulture();

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

        private static void SetUpCulture()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru-RU");
        }
    }
}
