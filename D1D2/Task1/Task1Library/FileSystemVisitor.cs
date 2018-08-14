using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Task1Library
{
    public class FileSystemVisitor : IEnumerable<string>
    {
        #region Fields
        private DirectoryInfo _directory;
        private Predicate<string> _filter;
        #endregion

        #region Events
        public event EventHandler<EventArgs> Start;
        public event EventHandler<EventArgs> Finish;
        public event EventHandler<FileEventArgs> FileFinded;
        public event EventHandler<DirectoryEventArgs> DirectoryFinded;
        public event EventHandler<FileEventArgs> FilteredFileFinded;
        public event EventHandler<DirectoryEventArgs> FilteredDirectoryFinded;
        #endregion

        #region Constructors
        public FileSystemVisitor(string path)
        {
            if (path == null) throw new ArgumentNullException();
            if (Directory.Exists(path) == false) throw new DirectoryNotFoundException();
            _directory = new DirectoryInfo(path);
        }

        public FileSystemVisitor(string path, Predicate<string> filter) : this(path)
        {
            _filter = filter ?? throw new ArgumentNullException();
        }
        #endregion

        #region Event calls
        protected virtual void OnStart(EventArgs args)
        {
            Start?.Invoke(this, args);
        }

        protected virtual void OnFinish(EventArgs args)
        {
            Finish?.Invoke(this, args);
        }

        protected virtual void OnFileFinded(FileEventArgs args)
        {
            FileFinded?.Invoke(this, args);
        }

        protected virtual void OnDirectoryFinded(DirectoryEventArgs args)
        {
            DirectoryFinded?.Invoke(this, args);
        }

        protected virtual void OnFilteredFileFinded(FileEventArgs args)
        {
            FilteredFileFinded?.Invoke(this, args);
        }

        protected virtual void OnFilteredDirectoryFinded(DirectoryEventArgs args)
        {
            FilteredDirectoryFinded?.Invoke(this, args);
        }
        #endregion

        #region Methods
        public IEnumerable<string> Find()
        {
            OnStart(EventArgs.Empty);
            foreach (var entry in _directory.EnumerateFileSystemInfos("*", SearchOption.AllDirectories))
            {
                var args = CallFindEvent(entry, false);
                if (args.Exclude) continue;
                if (_filter == null)
                {
                    yield return entry.Name;
                    if (args.Break) yield break;
                    continue;
                }

                if (_filter(entry.Name))
                {
                    args = CallFindEvent(entry, true);
                    if (args.Exclude) continue;
                    yield return entry.Name;
                    if (args.Break) yield break;
                }
            }
            OnFinish(EventArgs.Empty);
        }

        private FileSystemVisitorEventArgs CallFindEvent(FileSystemInfo item, bool isFilteredCall)
        {
            switch (item.GetType())
            {
                case Type type when typeof(DirectoryInfo) == type:
                    {
                        DirectoryEventArgs args = new DirectoryEventArgs() { Directory = item };
                        if (isFilteredCall)
                        {
                            OnFilteredDirectoryFinded(args);
                        }
                        else
                        {
                            OnDirectoryFinded(args);
                        }
                        return args;
                    }
                case Type type when typeof(FileInfo) == type:
                    {
                        FileEventArgs args = new FileEventArgs() { File = item };
                        if(isFilteredCall)
                        {
                            OnFilteredFileFinded(args);
                        }
                        else
                        {
                            OnFileFinded(args);
                        }              
                        return args;
                    }
                default: return null;
            }
        }

        public IEnumerator<string> GetEnumerator()
        {
            return Find().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
