using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Task4Library.EventArgs;
using Task4Library.Interfaces;

namespace Task4Library.Concrete
{
    public class FoldersListener : IFoldersListener
    {
        #region Fields
        private ILogger _logger;
        private IList<FileSystemWatcher> _watchers;
        #endregion

        #region Events
        public event EventHandler<FileCreatedEventArgs> FileCreated;
        #endregion

        #region Constructors
        public FoldersListener(IEnumerable<string> folders, ILogger logger)
        {
            _logger = logger;
            _watchers = folders.Select(CreateWatcher).ToList();
        }
        #endregion

        #region Methods
        public void StartListen()
        {
            foreach(var watcher in _watchers)
            {
                watcher.EnableRaisingEvents = true;
            }
        }

        public void StopListen()
        {
            foreach (var watcher in _watchers)
            {
                watcher.EnableRaisingEvents = false;
            }
        }

        private void File_Created(object sender, FileSystemEventArgs e)
        {
            _logger.Log($"File '{e.Name}' created at {File.GetCreationTime(e.FullPath)}");
            FileCreated?.Invoke(this, new FileCreatedEventArgs { FileName = e.Name, Path = e.FullPath, CreationDate = File.GetCreationTime(e.FullPath) });
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
