using System.IO;
using Task4Library.Interfaces;

namespace Task4Library.Concrete
{
    public class FilesMover : IFilesMover
    {
        #region Fields
        private ILogger _logger;
        #endregion

        #region Constructors
        public FilesMover(ILogger logger)
        {
            _logger = logger;
        }
        #endregion

        #region Methods
        public void MoveFile(string sourceFilePath, string destFilePath)
        {
            if (File.Exists(destFilePath))
            {
                File.Delete(destFilePath);
            }

            Directory.Move(sourceFilePath, destFilePath);
            _logger.Log($"File '{Path.GetFileName(sourceFilePath)}' moved to '{Path.GetDirectoryName(destFilePath)}' folder");
        }
        #endregion
    }
}
