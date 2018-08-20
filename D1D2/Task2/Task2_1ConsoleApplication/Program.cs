using System;

namespace Task2_1ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string usersInput;
            do
            {
                usersInput = GetInputFromUser();
                try
                {
                    PrintFirstCharacter(usersInput);
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("You type empty string! String should contain 1 character at minimum!");
                }
            }
            while (true);
        }

        static string GetInputFromUser()
        {
            Console.WriteLine("Type a string and press Enter!");
            var result = Console.ReadLine();
            return result;
        }

        static void PrintFirstCharacter(string inputString)
        {
            if (string.IsNullOrEmpty(inputString)) throw new ArgumentNullException();
            Console.WriteLine(inputString[0]);
        }
    }
}
