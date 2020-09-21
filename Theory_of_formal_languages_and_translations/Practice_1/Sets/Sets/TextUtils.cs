
namespace Sets
{
    public static class TextUtils
    {
        /// <summary>
        /// Находит все символы, которые встречаются в двух заданных текстах;
        /// </summary>
        /// <returns>множество символов, встречающихся в двух текстах</returns>
        public static Set<char> AllSymbolsInTexts(string text1, string text2)
        {
            Set<char> symbols = new Set<char>();

            foreach (var symbol in text1)
            {
                symbols.Add(symbol);
            }

            foreach (var symbol in text2)
            {
                symbols.Add(symbol);
            }

            return symbols;
        }

        /// <summary>
        /// Определить символы, которые встречаются в первом тексте, но не встречаются во втором.
        /// </summary>
        /// <returns>множество символов, встречающихся в первом тексте, но не во втором</returns>
        public static Set<char> SymbolsInFirstTextButNotInSecond(string text1, string text2)
        {
            Set<char> firstTextSymbols = new Set<char>();

            foreach (var symbol in text1)
            {
                firstTextSymbols.Add(symbol);
            }

            Set<char> secondTextSymbols = new Set<char>();

            foreach (var symbol in text2)
            {
                secondTextSymbols.Add(symbol);
            }

            firstTextSymbols.ExceptWith(secondTextSymbols);

            return firstTextSymbols;
        }

        /// <summary>
        /// Определяет сколько различных символов в тексте.
        /// </summary>
        public static int VariousSymbolsNumber(string text)
        {
            Set<char> symbols = new Set<char>();

            foreach (var symbol in text)
            {
                symbols.Add(symbol);
            }

            return symbols.Count;
        }
    }
}
