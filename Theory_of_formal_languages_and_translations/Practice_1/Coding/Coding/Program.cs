using System;
using System.IO;

namespace Coding
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string path = "..\\..\\..\\text.txt";
            string text;

            using (StreamReader reader = new StreamReader(path))
            {
                text = reader.ReadToEnd();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Text:");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(text);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nCodes:");
            Console.ForegroundColor = ConsoleColor.Yellow;
            CodingLogic coding = new CodingLogic(text);
            coding.Run();

            Console.ResetColor();
        }
    }
}
