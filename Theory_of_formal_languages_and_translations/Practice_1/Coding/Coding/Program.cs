using System;
using System.IO;
using System.Threading.Tasks;

namespace Coding
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string path = "..\\..\\..\\text1.txt";
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
