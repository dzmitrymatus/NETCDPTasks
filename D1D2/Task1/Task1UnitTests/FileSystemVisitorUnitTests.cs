using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Task1Library;

namespace Task1UnitTests
{
    [TestClass]
    public class FileSystemVisitorUnitTests
    {
        private string _currentDirectory = Environment.CurrentDirectory;

        [TestInitialize]
        public void Initialize()
        {
            Cleanup();
            var directory = Directory.CreateDirectory($"{_currentDirectory}\\RootFolder");
            var subDirectory1 = directory.CreateSubdirectory("SubDirectory1");
            var subDirectory2 = directory.CreateSubdirectory("SubDirectory2");
            var subDirectory3 = subDirectory2.CreateSubdirectory("SubDirectory2_1");
            File.Create($"{directory.FullName}\\File1.txt").Close();
            File.Create($"{subDirectory1.FullName}\\File2.cs").Close();
            File.Create($"{subDirectory1.FullName}\\File3.jpg").Close();
            File.Create($"{subDirectory2.FullName}\\File4.txt").Close();
            File.Create($"{subDirectory2.FullName}\\File5.dll").Close();
        }

        [TestMethod]
        public void CheckDirectoryNotFoundException()
        {
            Assert.ThrowsException<DirectoryNotFoundException>(() => new FileSystemVisitor($"{_currentDirectory}\\NotFoundFolder"));
        }

        [TestMethod]
        public void CheckDirectoryArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new FileSystemVisitor(null));
        }

        [TestMethod]
        public void CheckFilterArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new FileSystemVisitor($"{_currentDirectory}\\RootFolder", null));
        }

        [TestMethod]
        public void CheckAllFilesWithoutFilter()
        {
            #region Arrange
            List<string> expectedList = new List<string>
            {
                "File1.txt",
                "SubDirectory1",
                "SubDirectory2",
                "File2.cs",
                "File3.jpg",
                "File4.txt",
                "File5.dll",
                "SubDirectory2_1"
            };
            FileSystemVisitor visitor = new FileSystemVisitor($"{_currentDirectory}\\RootFolder");
            #endregion

            #region Act
            var actualList = visitor.Find().ToList();
            #endregion

            #region Assert
            CollectionAssert.AreEqual(expectedList, actualList, $"Actual value is '{string.Join(", ",actualList)}'");
            #endregion
        }

        [TestMethod]
        public void CheckAllFilesWithFilter()
        {
            #region Arrange
            Predicate<string> filter = x => x.EndsWith("txt");
            List<string> expectedList = new List<string>
            {
                "File1.txt",
                "File4.txt"
            };
            FileSystemVisitor visitor = new FileSystemVisitor($"{_currentDirectory}\\RootFolder", filter);
            #endregion

            #region Act
            var actualList = visitor.Find().ToList();
            #endregion

            #region Assert
            CollectionAssert.AreEqual(expectedList, actualList, $"Actual value is '{string.Join(", ", actualList)}'");
            #endregion
        }

        [TestMethod]
        public void CheckBreakSearchAfterFileFindedEvent()
        {
            #region Arrange
            EventHandler<FileEventArgs> fileFindedHandler = (sender, e) => 
            {
                if(e.File.Name == "File2.cs")
                {
                    e.Break = true;
                }
            };

            List<string> expectedList = new List<string>
            {
                "File1.txt",
                "SubDirectory1",
                "SubDirectory2",
                "File2.cs"
            };
            FileSystemVisitor visitor = new FileSystemVisitor($"{_currentDirectory}\\RootFolder");
            #endregion

            #region Act
            visitor.FileFinded += fileFindedHandler;
            var actualList = visitor.Find().ToList();
            #endregion

            #region Assert
            CollectionAssert.AreEqual(expectedList, actualList, $"Actual value is '{string.Join(", ", actualList)}'");
            #endregion
        }

        [TestMethod]
        public void CheckExcludeFileAfterFileFindedEvent()
        {
            #region Arrange
            EventHandler<FileEventArgs> fileFindedHandler = (sender, e) =>
            {
                if (e.File.Name == "File2.cs")
                {
                    e.Exclude = true;
                }
            };

            List<string> expectedList = new List<string>
            {
               "File1.txt",
                "SubDirectory1",
                "SubDirectory2",
                "File3.jpg",
                "File4.txt",
                "File5.dll",
                "SubDirectory2_1"
            };
            FileSystemVisitor visitor = new FileSystemVisitor($"{_currentDirectory}\\RootFolder");
            #endregion

            #region Act
            visitor.FileFinded += fileFindedHandler;
            var actualList = visitor.Find().ToList();
            #endregion

            #region Assert
            CollectionAssert.AreEqual(expectedList, actualList, $"Actual value is '{string.Join(", ", actualList)}'");
            #endregion
        }

        [TestMethod]
        public void CheckBreakSearchAfterDirectoryFindedEvent()
        {
            #region Arrange
            EventHandler<DirectoryEventArgs> directoryFindedHandler = (sender, e) =>
            {
                if (e.Directory.Name == "SubDirectory2")
                {
                    e.Break = true;
                }
            };

            List<string> expectedList = new List<string>
            {
                "File1.txt",
                "SubDirectory1",
                "SubDirectory2"
            };
            FileSystemVisitor visitor = new FileSystemVisitor($"{_currentDirectory}\\RootFolder");
            #endregion

            #region Act
            visitor.DirectoryFinded += directoryFindedHandler;
            var actualList = visitor.Find().ToList();
            #endregion

            #region Assert
            CollectionAssert.AreEqual(expectedList, actualList, $"Actual value is '{string.Join(", ", actualList)}'");
            #endregion
        }

        [TestMethod]
        public void CheckExcludeDirectoryAfterDirectoryFindedEvent()
        {
            #region Arrange
            EventHandler<DirectoryEventArgs> directoryFindedHandler = (sender, e) =>
            {
                if (e.Directory.Name == "SubDirectory2")
                {
                    e.Exclude = true;
                }
            };

            List<string> expectedList = new List<string>
            {
                "File1.txt",
                "SubDirectory1",
                "File2.cs",
                "File3.jpg",
                "File4.txt",
                "File5.dll",
                "SubDirectory2_1"
            };
            FileSystemVisitor visitor = new FileSystemVisitor($"{_currentDirectory}\\RootFolder");
            #endregion

            #region Act
            visitor.DirectoryFinded += directoryFindedHandler;
            var actualList = visitor.Find().ToList();
            #endregion

            #region Assert
            CollectionAssert.AreEqual(expectedList, actualList, $"Actual value is '{string.Join(", ", actualList)}'");
            #endregion
        }

        [TestMethod]
        public void CheckBreakSearchAfterFilteredFileFindedEvent()
        {
            #region Arrange
            Predicate<string> filter = x => x.EndsWith(".txt");

            EventHandler<FileEventArgs> filteredFileFindedHandler = (sender, e) =>
            {
                if (e.File.Name.StartsWith("File1"))
                {
                    e.Break = true;
                }
            };

            List<string> expectedList = new List<string>
            {
                "File1.txt"
            };

            FileSystemVisitor visitor = new FileSystemVisitor($"{_currentDirectory}\\RootFolder", filter);
            #endregion

            #region Act
            visitor.FilteredFileFinded += filteredFileFindedHandler;
            var actualList = visitor.Find().ToList();
            #endregion

            #region Assert
            CollectionAssert.AreEqual(expectedList, actualList, $"Actual value is '{string.Join(", ", actualList)}'");
            #endregion
        }

        [TestMethod]
        public void CheckExcludeFileAfterFilteredFileFindedEvent()
        {
            #region Arrange
            Predicate<string> filter = x => x.EndsWith(".txt");

            EventHandler<FileEventArgs> filteredFileFindedHandler = (sender, e) =>
            {
                if (e.File.Name.StartsWith("File1"))
                {
                    e.Exclude = true;
                }
            };

            List<string> expectedList = new List<string>
            {
                "File4.txt"
            };

            FileSystemVisitor visitor = new FileSystemVisitor($"{_currentDirectory}\\RootFolder", filter);
            #endregion

            #region Act
            visitor.FilteredFileFinded += filteredFileFindedHandler;
            var actualList = visitor.Find().ToList();
            #endregion

            #region Assert
            CollectionAssert.AreEqual(expectedList, actualList, $"Actual value is '{string.Join(", ", actualList)}'");
            #endregion
        }

        [TestMethod]
        public void CheckBreakSearchAfterFilteredDirectoryFindedEvent()
        {
            #region Arrange
            Predicate<string> filter = x => x.StartsWith("SubDirectory");

            EventHandler<DirectoryEventArgs> filteredDirectoryFindedHandler = (sender, e) =>
            {
                if (e.Directory.Name.EndsWith("2"))
                {
                    e.Break = true;
                }
            };

            List<string> expectedList = new List<string>
            {
                "SubDirectory1",
                "SubDirectory2"
            };

            FileSystemVisitor visitor = new FileSystemVisitor($"{_currentDirectory}\\RootFolder", filter);
            #endregion

            #region Act
            visitor.FilteredDirectoryFinded += filteredDirectoryFindedHandler;
            var actualList = visitor.Find().ToList();
            #endregion

            #region Assert
            CollectionAssert.AreEqual(expectedList, actualList, $"Actual value is '{string.Join(", ", actualList)}'");
            #endregion
        }

        [TestMethod]
        public void CheckExcludeDirectoryAfterFilteredDirectoryFindedEvent()
        {
            #region Arrange
            Predicate<string> filter = x => x.StartsWith("SubDirectory");

            EventHandler<DirectoryEventArgs> filteredDirectoryFindedHandler = (sender, e) =>
            {
                if (e.Directory.Name.EndsWith("2"))
                {
                    e.Exclude = true;
                }
            };

            List<string> expectedList = new List<string>
            {
                "SubDirectory1",
                "SubDirectory2_1"
            };

            FileSystemVisitor visitor = new FileSystemVisitor($"{_currentDirectory}\\RootFolder", filter);
            #endregion

            #region Act
            visitor.FilteredDirectoryFinded += filteredDirectoryFindedHandler;
            var actualList = visitor.Find().ToList();
            #endregion

            #region Assert
            CollectionAssert.AreEqual(expectedList, actualList, $"Actual value is '{string.Join(", ", actualList)}'");
            #endregion
        }

        [TestCleanup]
        public void Cleanup()
        {
            if(Directory.Exists($"{_currentDirectory}\\RootFolder"))
            {
                Directory.Delete($"{_currentDirectory}\\RootFolder", true);
            }
        }
    }
}
