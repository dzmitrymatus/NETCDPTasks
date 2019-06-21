using System.IO;
using Task4Library.Logger;

namespace Task4Library.FilesMover.Concrete
{
    public class DefaultFilesMover : IFilesMover
    {
        #region Fields
        private ILogger _logger;
        #endregion

        #region Constructors
        public DefaultFilesMover(ILogger logger)
        {
            _logger = logger;
        }
        #endregion

        #region Methods
        public void MoveFile(string sourceFilePath, string destinationFolderPath)
        {
            var fileName = Path.GetFileName(sourceFilePath);
            var destinationFilePath = Path.Combine(destinationFolderPath, fileName);
            if (File.Exists(destinationFilePath))
            {
                File.Delete(destinationFilePath);
            }

            Directory.Move(sourceFilePath, destinationFilePath);
            _logger.Log($"File '{Path.GetFileName(sourceFilePath)}' moved to '{Path.GetDirectoryName(destinationFilePath)}' folder");
        }
        #endregion
    }
}
