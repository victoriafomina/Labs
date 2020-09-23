using System;
using System.IO;

namespace AlphabeticalIndex
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
            AlphabeticalIndex alphabeticalIndex = new AlphabeticalIndex(text);
            
            foreach (var word in alphabeticalIndex.GetAlphabeticalIndex())
            {
                Console.Write($"({word.Item1}):");

                for (var i = 0; i < word.Item2.Count; ++i)
                {
                    Console.Write($" {word.Item2[i]},");
                }
                Console.WriteLine();
            }

            Console.ResetColor();
        }
    }
}
