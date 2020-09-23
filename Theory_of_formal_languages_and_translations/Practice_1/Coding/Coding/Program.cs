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

            CodingLogic coding = new CodingLogic(text);
            coding.Run();
        }
    }
}
