using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Task4Library.Interfaces;
using Task4Library.Models;

namespace Task4Library.Concrete
{
    public class FilesMoverService
    {
        #region Fields
        private ILogger _logger;
        private IFilesMover _filesMover;
        private IFoldersListener _foldersListener;
        private IEnumerable<Rule> _rules;
        private string _defaultDestinationFolder;
        #endregion

        #region Constructors
        public FilesMoverService(IEnumerable<string> folders, IEnumerable<Rule> rules, string defaultDestinationFolder)
        {
            _logger = new ConsoleLogger();
            _filesMover = new FilesMover(_logger);
            _rules = rules;
            _defaultDestinationFolder = defaultDestinationFolder;
            _foldersListener = new FoldersListener(folders, _logger);
            _foldersListener.FileCreated += _foldersListener_FileCreated;
        }
        #endregion

        #region Methods
        public void Start()
        {
            _foldersListener.StartListen();
            _logger.Log("Service is working...");
        }

        public void Stop()
        {
            _foldersListener.StopListen();
            _logger.Log("Service has stoped...");
        }

        private void _foldersListener_FileCreated(object sender, EventArgs.FileCreatedEventArgs e)
        {
            var rule = FindRule(e.FileName);
            _filesMover.MoveFile(e.Path, Path.Combine(rule.DestinationFolder, e.FileName));
        }

        private Rule FindRule(string fileName)
        {
            var rule = _rules.FirstOrDefault(x => Regex.IsMatch(fileName, x.FileNameRegex));
            if (rule != null)
            {
                _logger.Log($"Rule for file '{fileName}' found");
            }
            else
            {
                _logger.Log($"Rule for file '{fileName}' not found");
                rule = new Rule() { DestinationFolder = _defaultDestinationFolder };
            }
            return rule;
        }
        #endregion
    }
}
