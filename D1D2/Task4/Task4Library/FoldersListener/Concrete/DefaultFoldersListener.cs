using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Task4Library.FoldersListener.EventArgs;
using Task4Library.Logger;

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
                _logger.Log($"Start listen folder '{watcher.Path}'.");
                watcher.EnableRaisingEvents = true;
            }
        }

        public void StopListen()
        {
            foreach (var watcher in _watchers)
            {
                _logger.Log($"Stop listen folder '{watcher.Path}'.");
                watcher.EnableRaisingEvents = false;
            }
        }

        private void File_Created(object sender, FileSystemEventArgs e)
        {
            _logger.Log($"File '{e.Name}' created at {File.GetCreationTime(e.FullPath)}");
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
