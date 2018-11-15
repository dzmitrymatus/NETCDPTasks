using System;

namespace Task4Library.EventArgs
{
    public class FileCreatedEventArgs : System.EventArgs
    {
        public string FileName { get; set; }
        public string Path { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
