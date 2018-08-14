using System;

namespace Task1Library
{
    public class FileSystemVisitorEventArgs : EventArgs
    {
        public bool Break { get; set; }
        public bool Exclude { get; set; }
    }
}
