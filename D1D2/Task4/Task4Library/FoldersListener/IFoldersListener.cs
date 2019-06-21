using System;

namespace Task4Library.FoldersListener
{
    public interface IFoldersListener
    {
        event EventHandler<FileCreatedEventArgs> FileCreated;
        void StartListen();
        void StopListen();
    }

    public class FileCreatedEventArgs : System.EventArgs
    {
        public string FileName { get; set; }
        public string Path { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
