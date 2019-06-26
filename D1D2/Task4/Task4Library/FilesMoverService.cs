using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Task4Library.Logger;
using Task4Library.Logger.Concrete;
using Task4Library.FilesMover;
using Task4Library.FilesMover.Concrete;
using Task4Library.FoldersListener;
using Task4Library.FoldersListener.Concrete;
using Task4Library.Resources.FilesMoverService;

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
        public FilesMoverService(FilesMoverServiceOptions options)
        {
            _rules = options.Rules;
            _defaultDestinationFolder = options.DefaultDestinationFolder;

            _logger = new ConsoleLogger();
            _filesMover = new DefaultFilesMover(_logger);
            _foldersListener = new DefaultFoldersListener(options.Folders, _logger);
            _foldersListener.FileCreated += ProccessCreatedFile;
        }

        public FilesMoverService(FilesMoverServiceOptions options,
             ILogger logger, IFilesMover filesMover, IFoldersListener foldersListener)
        {
            _rules = options.Rules;
            _defaultDestinationFolder = options.DefaultDestinationFolder;
            _logger = logger;
            _filesMover = filesMover;
            _foldersListener = foldersListener;
            _foldersListener.FileCreated += ProccessCreatedFile;
        }
        #endregion

        #region Methods
        public void Start()
        {
            _foldersListener.StartListen();
            _logger.Log(FilesMoverServiceResource.StartLogMessage);
        }

        public void Stop()
        {
            _foldersListener.StopListen();
            _logger.Log(FilesMoverServiceResource.StopLogMessage);
        }

        private void ProccessCreatedFile(object sender, FileCreatedEventArgs e)
        {
            var rule = FindRule(e.FileName);
            _filesMover.MoveFile(e.Path, rule.DestinationFolder, rule.isAddNumber, rule.isAddMoveDate);
        }

        private Rule FindRule(string fileName)
        {
            var rule = _rules.FirstOrDefault(x => x.FileNameRegex.IsMatch(fileName));
            if (rule != null)
            {
                _logger.Log(string.Format(FilesMoverServiceResource.RuleFindedLogMessage, fileName));
            }
            else
            {
                _logger.Log(string.Format(FilesMoverServiceResource.RuleNotFindedLogMessage, fileName));
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
        public bool isAddNumber { get; set; }
        public bool isAddMoveDate { get; set; }
    }

    public class FilesMoverServiceOptions
    {
        public IEnumerable<string> Folders { get; set; }
        public string DefaultDestinationFolder { get; set; }
        public IEnumerable<Rule> Rules { get; set; }
    }
}
