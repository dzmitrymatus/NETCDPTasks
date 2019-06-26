using System.IO;
using System.Text.RegularExpressions;

namespace Task4Library.FilesMover.Concrete
{
    public static class StringExtensions
    {
        public static string CleanIllegalName(this string name)
        {
            return new Regex($"[{Regex.Escape(new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars()))}]")
                .Replace(name, "_");
        }
    }
}
