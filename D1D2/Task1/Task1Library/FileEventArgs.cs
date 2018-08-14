using System.IO;

namespace Task1Library
{
    public class FileEventArgs : FileSystemVisitorEventArgs
    {
        public FileSystemInfo File { get; set; }
    }
}
