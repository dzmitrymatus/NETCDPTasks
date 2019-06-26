namespace Task4Library.FilesMover
{
    public interface IFilesMover
    {
        void MoveFile(string sourceFilePath, string destFilePath, bool isAddNumber, bool isAddMoveDate);
    }
}
