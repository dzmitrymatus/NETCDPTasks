using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using Task1Library;

namespace Task1UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        string currentDirectory = Environment.CurrentDirectory;

        [TestInitialize]
        public void Initialize()
        {
            Cleanup();
            var directory = Directory.CreateDirectory($"{currentDirectory}\\RootFolder");
            var subDirectory1 = directory.CreateSubdirectory("SubDirectory1");
            var subDirectory2 = directory.CreateSubdirectory("SubDirectory2");
            File.Create($"{directory.FullName}\\File1.txt").Close();
            File.Create($"{subDirectory1.FullName}\\File2.cs").Close();
            File.Create($"{subDirectory1.FullName}\\File3.jpg").Close();
            File.Create($"{subDirectory2.FullName}\\File4.txt").Close();
            File.Create($"{subDirectory2.FullName}\\File5.dll").Close();
        }

        [TestMethod]
        public void TestMethod1()
        {
            //to do
            FileSystemVisitor visitor = new FileSystemVisitor($"{currentDirectory}\\RootFolder");
        }

        [TestCleanup]
        public void Cleanup()
        {
            if(Directory.Exists($"{currentDirectory}\\RootFolder"))
            {
                Directory.Delete($"{currentDirectory}\\RootFolder", true);
            }
        }
    }
}
