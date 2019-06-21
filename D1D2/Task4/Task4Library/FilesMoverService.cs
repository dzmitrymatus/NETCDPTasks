using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Task4Library.Logger;
using Task4Library.Logger.Concrete;
using Task4Library.FilesMover;
using Task4Library.FilesMover.Concrete;
using Task4Library.FoldersListener;
using Task4Library.FoldersListener.Concrete;

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
            _rules = rules;
            _defaultDestinationFolder = defaultDestinationFolder;

            _logger = new ConsoleLogger();
            _filesMover = new DefaultFilesMover(_logger);
            _foldersListener = new DefaultFoldersListener(folders, _logger);
            _foldersListener.FileCreated += ProccessCreatedFile;
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

        private void ProccessCreatedFile(object sender, FileCreatedEventArgs e)
        {
            var rule = FindRule(e.FileName);
            //to do
            _filesMover.MoveFile(e.Path, rule.DestinationFolder);
        }

        private Rule FindRule(string fileName)
        {
            var rule = _rules.FirstOrDefault(x => x.FileNameRegex.IsMatch(fileName));
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

    public class Rule
    {
        public Regex FileNameRegex { get; set; }
        public string DestinationFolder { get; set; }
        //public bool isAddNumber { get; set; }
        //public bool isAddMoveDate { get; set; }
    }
}
