using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Task4Library
{
    public class FoldersListener
    {
        private IEnumerable<FoldersListenerRule> rules;
        private IFolderListenerLogger logger;
        private List<FileSystemWatcher> watchers = new List<FileSystemWatcher>();
        private string defaultDestinationFolder;

        public FoldersListener(IEnumerable<string> folders, IEnumerable<FoldersListenerRule> rules, IFolderListenerLogger logger, string defaultDestinationFolder)
        {
            this.rules = rules;
            this.logger = logger;
            this.defaultDestinationFolder = defaultDestinationFolder;
            foreach (var folder in folders)
            {
                if (Directory.Exists(folder) == false) throw new DirectoryNotFoundException();

                var watcher = new FileSystemWatcher(folder) { EnableRaisingEvents = true };
                watcher.Created += File_Created;
                watchers.Add(watcher);
            }
        }

        private void File_Created(object sender, FileSystemEventArgs e)
        {
            logger.Log($"File '{e.Name}' created at {DateTime.Now.ToString()}");

            var rule = FindRule(e.Name);
            MoveFile(e.FullPath, Path.Combine(rule.DestinationFolder, e.Name));
        }

        private FoldersListenerRule FindRule(string fileName)
        {
            var rule = rules.FirstOrDefault(x => Regex.IsMatch(fileName, x.FileNameRegex));
            if (rule != null)
            {
                logger.Log($"Rule for file '{fileName}' found");
            }
            else
            {
                logger.Log($"Rule for file '{fileName}' not found");
                rule = new FoldersListenerRule() { DestinationFolder = defaultDestinationFolder };
            }
            return rule;
        }

        private void MoveFile(string sourceFilePath, string destFilePath)
        {
            if(File.Exists(destFilePath))
            {
                File.Delete(destFilePath);
            }

            Directory.Move(sourceFilePath, destFilePath);
            logger.Log($"File '{Path.GetFileName(sourceFilePath)}' moved to '{Path.GetDirectoryName(destFilePath)}' folder");
        }


    }
}
