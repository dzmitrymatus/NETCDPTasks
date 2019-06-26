using System.Text.RegularExpressions;

namespace Task4ConsoleApplication.Configuration.Models
{
    public class Rule
    {
        public Regex Template { get; set; }
        public string DestinationFolder { get; set; }
        public bool IsAddNumber { get; set; }
        public bool IsAddMoveDate { get; set; }
    }
}
