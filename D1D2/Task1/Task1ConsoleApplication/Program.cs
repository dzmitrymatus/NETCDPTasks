using System;
using Task1Library;

namespace Task1ConsoleApplication
{
    public class Program
    {
        static void Main(string[] args)
        {
            string path = @"D:\GitHub\NETCDPTasks\D1D2\Task1";
            var fileSystemVisitor = new FileSystemVisitor(path, x=> x.Contains(".cs"));

            fileSystemVisitor.FileFinded += FileSystemVisitor_FileFinded;
            fileSystemVisitor.FilteredFileFinded += FileSystemVisitor_FilteredFileFinded;



            foreach(var item in fileSystemVisitor)
            {
                Console.WriteLine(item);
            }
           
            Console.ReadLine();
        }

        private static void FileSystemVisitor_FilteredFileFinded(object sender, FileEventArgs e)
        {
            Console.WriteLine($"{e.File.Name} is finded!");
           if(e.File.Name == "UnitTest1.cs") e.Exclude = true;
        }

        private static void FileSystemVisitor_FileFinded(object sender, FileEventArgs e)
        {
           // Console.WriteLine($"{e.File.Name} is finded!");
           // e.Break = true;
        }
    }
}
