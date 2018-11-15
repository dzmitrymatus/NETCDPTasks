using System;
using Task4Library.EventArgs;

namespace Task4Library.Interfaces
{
    public interface IFoldersListener
    {
        event EventHandler<FileCreatedEventArgs> FileCreated;
        void StartListen();
        void StopListen();
    }
}
