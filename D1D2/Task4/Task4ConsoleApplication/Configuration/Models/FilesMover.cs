using System.Collections.Generic;
using System.Globalization;

namespace Task4ConsoleApplication.Configuration.Models
{
    public class FilesMover
    {
        public CultureInfo Culture { get; set; }
        public string DefaultDestinationFolder { get; set; }
        public List<string> Folders { get; set; }
        public List<Rule> Rules { get; set; }
    }
}
