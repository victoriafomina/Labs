using System.Collections.Generic;

namespace AlphabeticalIndex
{
    public class AlphabeticalIndex
    {
        private string text;

        private List<string> splitedText;

        private char[] separators = { ' ', '\t', '\r', '.', ',', ';', '-', '−', '–', '—', '\'' };

        private List<(string, List<int>)> alphabeticalIndex;

        public AlphabeticalIndex(string text)
        {
            this.text = text.ToLower();
            this.splitedText = new List<string>();
            this.alphabeticalIndex = new List<(string, List<int>)>();
            SplitText();
            CreateAlphabeticalIndex();
            alphabeticalIndex.Sort();
        }

        private void SplitText()
        {
            string[] splitedTextFirstVersion = text.Split(separators);

            for (var i = 0; i < splitedTextFirstVersion.Length; ++i)
            {
                if (splitedTextFirstVersion[i] != "" && splitedTextFirstVersion[i][0] != '\n')
                {
                    splitedText.Add(splitedTextFirstVersion[i]);
                }
                else if (splitedTextFirstVersion[i] != "" && splitedTextFirstVersion[i][0] == '\n')
                {
                    splitedText.Add("\n");

                    if (splitedTextFirstVersion[i].Length > 1)
                    {                        
                        splitedText.Add(splitedTextFirstVersion[i].Substring(1));
                    }
                }
            }
        }

        public List<(string, List<int>)> GetAlphabeticalIndex() => alphabeticalIndex;

        private void CreateAlphabeticalIndex()
        {
            int stringNumber = 1;

            for (var i = 0; i < splitedText.Count; ++i)
            {
                if (splitedText[i] == "\n")
                {
                    ++stringNumber;
                }
                else if (WordExistsInAlphabeticalIndex(splitedText[i]))
                {
                    int wordIndex = WordIndexInAlphabeticalIndex(splitedText[i]);
                    alphabeticalIndex[wordIndex].Item2.Add(stringNumber);
                }
                else
                {
                    alphabeticalIndex.Add((splitedText[i], new List<int> { stringNumber }));
                }
            }
        }

        private int WordIndexInAlphabeticalIndex(string word)
        {
            int index = -1;

            for (var i = 0; i < alphabeticalIndex.Count; ++i)
            {
                if (alphabeticalIndex[i].Item1 == word)
                {
                    index = i;
                    return index;
                }
            }

            return index;
        }

        private bool WordExistsInAlphabeticalIndex(string word)
        {
            foreach (var wrd in alphabeticalIndex)
            {
                if (wrd.Item1 == word)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
