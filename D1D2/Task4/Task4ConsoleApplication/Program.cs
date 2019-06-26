using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Task4ConsoleApplication.Configuration;
using Task4ConsoleApplication.Configuration.Models;
using Task4Library.Concrete;

namespace Task4ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var filesMoverConfiguration = ApplicationConfiguration.FilesMoverConfiguration;
            SetUpCulture(filesMoverConfiguration.Culture);
            var filesMoverServiceOptions = SetUpOptions(filesMoverConfiguration);
            var service = new FilesMoverService(filesMoverServiceOptions);

            service.Start();

            CancellationTokenSource source = new CancellationTokenSource();
            Console.CancelKeyPress += (o, e) =>
            {
                source.Cancel();
                service.Stop();
            };
            Task.Delay(TimeSpan.FromMilliseconds(-1), source.Token).Wait();
        }

        private static FilesMoverServiceOptions SetUpOptions(FilesMover options)
        {
            return new FilesMoverServiceOptions
            {
                DefaultDestinationFolder = options.DefaultDestinationFolder,
                Folders = options.Folders,
                Rules = options.Rules.Select(x => new Task4Library.Concrete.Rule
                {
                    DestinationFolder = x.DestinationFolder,
                    FileNameRegex = x.Template,
                    isAddMoveDate = x.IsAddMoveDate,
                    isAddNumber = x.IsAddNumber
                })
            };
        }

        private static void SetUpCulture(CultureInfo cultureInfo)
        {
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }
    }
}
