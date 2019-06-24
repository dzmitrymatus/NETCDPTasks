using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Task4Library.Logger;
using Task4Library.Resources.FoldersListener;

namespace Task4Library.FoldersListener.Concrete
{
    public class DefaultFoldersListener : IFoldersListener
    {
        #region Fields
        private ILogger _logger;
        private IEnumerable<FileSystemWatcher> _watchers;
        #endregion

        #region Events
        public event EventHandler<FileCreatedEventArgs> FileCreated;
        #endregion

        #region Constructors
        public DefaultFoldersListener(IEnumerable<string> folders, ILogger logger)
        {
            _logger = logger;
            _watchers = folders.Select(CreateWatcher);
        }
        #endregion

        #region Methods
        public void StartListen()
        {
            foreach(var watcher in _watchers)
            {
                _logger.Log(string.Format(FoldersListenerResource.StartListenLogMessage, watcher.Path));
                watcher.EnableRaisingEvents = true;
            }
        }

        public void StopListen()
        {
            foreach (var watcher in _watchers)
            {
                _logger.Log(string.Format(FoldersListenerResource.StopListenLogMessage, watcher.Path));
                watcher.EnableRaisingEvents = false;
            }
        }

        protected virtual void File_Created(object sender, FileSystemEventArgs e)
        {
            _logger.Log(string.Format(FoldersListenerResource.FileCreatedEventLogMessage,
                e.Name,
                File.GetCreationTime(e.FullPath).ToString(Thread.CurrentThread.CurrentUICulture.DateTimeFormat)));
            FileCreated?.Invoke(this, new FileCreatedEventArgs
            { FileName = e.Name,
              Path = e.FullPath,
              CreationDate = File.GetCreationTime(e.FullPath)
            });
        }

        private FileSystemWatcher CreateWatcher(string path)
        {
            if (Directory.Exists(path) == false) throw new DirectoryNotFoundException();

            var watcher = new FileSystemWatcher(path);
            watcher.Created += File_Created;
            return watcher;
        }
        #endregion
    }
}
