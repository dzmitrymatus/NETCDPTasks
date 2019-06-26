using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;
using Task4ConsoleApplication.Configuration.Models;
using RockLib.Configuration.ObjectFactory;

namespace Task4ConsoleApplication.Configuration
{
    public static class ApplicationConfiguration
    {
        private static IConfiguration _configuration;

        static ApplicationConfiguration()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public static FilesMover FilesMoverConfiguration
        {
            get
            {
                var valueConverters = new ValueConverters().Add(typeof(Regex), x => new Regex(x));
                return _configuration
                    .GetSection("FilesMover")
                    .Create<FilesMover>(valueConverters: valueConverters);
            }
        }
    }
}
