using System.IO;

namespace Task1Library
{
    public class DirectoryEventArgs : FileSystemVisitorEventArgs
    {
        public FileSystemInfo Directory { get; set; }
    }
}
