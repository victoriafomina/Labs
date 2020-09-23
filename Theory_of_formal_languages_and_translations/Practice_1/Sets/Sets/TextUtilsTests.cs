using System;
using System.Collections.Generic;
using System.Text;

namespace Sets
{
    public static class TextUtilsTests
    {
        public static void SymbolsInFirstTextButNotInSecondTest()
        {
            string text1 = "my mistress eyes";
            string text2 = "are nothing like the sun";

            Console.WriteLine($"Первый текст:\n{text1}\n");
            Console.WriteLine($"Второй текст:\n{text2}");

            Set<char> symbols = TextUtils.SymbolsInFirstTextButNotInSecond(text1, text2);

            Console.WriteLine("\nСимволы, которые встречаются в первом тексте, но не во втором:");
            foreach (var symbol in symbols)
            {
                Console.Write($"{symbol}, ");
            }

            Console.WriteLine("\n--------------------------------------------------------\n");
        }

        public static void VariousSymbolsNumberTest()
        {
            string text = "We are hoping for the best";
            Console.WriteLine($"Текст:\n{text}");

            var variousSymbolsNumber = TextUtils.VariousSymbolsNumber(text);
            Console.WriteLine($"\nРазличных символов в тексте: {variousSymbolsNumber}");

            Console.WriteLine("--------------------------------------------------------\n");
        }

        public static void AllSymbolsInTextsTest()
        {
            string text1 = "my mistress eyes";
            string text2 = "are nothing like the sun";

            Console.WriteLine($"Первый текст:\n{text1}\n");
            Console.WriteLine($"Второй текст:\n{text2}");

            Console.WriteLine("\nВсе символы, которые встречаются в двух заданных текстах:");
            Set<char> symbols = TextUtils.AllSymbolsInTexts(text1, text2);
            foreach (var symbol in symbols)
            {
                Console.Write($"{symbol}, ");
            }

            Console.WriteLine("\n--------------------------------------------------------\n");
        }
    }
}
