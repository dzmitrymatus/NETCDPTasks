using System;
using Task1Library;

namespace Task1ConsoleApplication
{
    public class Program
    {
        static void Main(string[] args)
        {
            var fileSystemVisitor = new FileSystemVisitor();
            Console.WriteLine(fileSystemVisitor.SayHi());
            Console.ReadLine();
        }
    }
}
