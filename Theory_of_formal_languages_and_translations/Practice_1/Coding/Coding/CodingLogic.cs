using System;
using System.Collections.Generic;
using System.Linq;

namespace Coding
{
    public class CodingLogic
    {
        private List<string> symbols;
        private char[] separators;
        private List<(string, int)> terminals = new List<(string, int)>();
        private List<(string, int)> nonterminals = new List<(string, int)>();
        private List<(string, int)> semantics = new List<(string, int)>();
        private const int firstNonterminalIndex = 11;
        private const int lastNonterminalIndex = 50;
        private const int firstSemanticIndex = 101;
        private const int lastSemanticIndex = 150;
        private const int firstTerminalIndex = 51;
        private const int lastTerminalIndex = 100;
        private const int eofgramIndex = 1000;
        private List<string> splitedText = new List<string>();
        private string text;

        public CodingLogic(string text)
        {
            this.text = text;
            symbols = new List<string> { ":", "(", ")", ".", "*", ";", ",", "#", "[", "]" };
            separators = new char[] { ':', '(', ')', '.', '*', ';', ',', '#', '[', ']', '\'', '\r', '\t', '\n', '$', ' ' };

            SplitTextBySeparators();
            splitedText.RemoveAll(x => x == " " || x == "\t" || x == "\r");
        }

        public void Run()
        {
            for (var i = 0; i < splitedText.Count; ++i)
            {
                if (splitedText[i] == "\'")
                {
                    WordHandler(i + 1, terminals, "terminals", firstTerminalIndex, lastTerminalIndex);
                    Console.Write($"{GetWordCode(splitedText[i + 1], terminals)}, ");
                    i += 2;
                }
                else if (splitedText[i] == "$")
                {
                    WordHandler(i + 1, semantics, "semantics", firstSemanticIndex, lastSemanticIndex);
                    Console.Write($"{GetWordCode(splitedText[i + 1], semantics)}, ");
                    ++i;
                }
                else if (splitedText[i] == ":")
                {
                    WordHandler(i - 1, nonterminals, "nonterminals", firstNonterminalIndex, lastNonterminalIndex);
                    Console.Write($"{GetWordCode(splitedText[i - 1], nonterminals)}, {symbols.IndexOf(splitedText[i]) + 1}, ");
                }
                else if (symbols.Contains(splitedText[i]))
                {
                    Console.Write($"{symbols.IndexOf(splitedText[i]) + 1}, ");
                }
                else if (splitedText[i] == "Eofgram" || splitedText[i] == "eofgram")
                {
                    Console.WriteLine(eofgramIndex);
                }
                else if (Char.IsLetter((splitedText[i])[0]) && splitedText[i + 1] != ":")
                {
                    WordHandler(i, terminals, "terminals", firstTerminalIndex, lastTerminalIndex);
                    Console.Write($"{GetWordCode(splitedText[i], terminals)}, ");
                }
                else if (splitedText[i] == "\n")
                {
                    Console.WriteLine();
                }            
            }
        }

        private void WordHandler(int index, List<(string, int)> words, string typeOfWords, int firstWordIndex, int lastWordIndex)
        {
            var currentWord = splitedText[index];

            if (WordAlreadyExists(currentWord, words))
            {
                var codeOfWord = GetWordCode(currentWord, words);
                Console.Write($"{codeOfWord}, ");
            }
            else
            {
                if (words.Count >= lastWordIndex - firstWordIndex + 1)
                {
                    throw new InvalidOperationException($"You can not add more {typeOfWords}!");
                }

                words.Add((currentWord, firstWordIndex + words.Count));
            }
        }

        private int GetWordCode(string word, List<(string, int)> words)
        {
            if (!WordAlreadyExists(word, words))
            {
                throw new InvalidOperationException($"The word \"{word}\" does not exists in code table!\n");
            }

            for (var i = 0; i < words.Count; ++i)
            {
                if (words[i].Item1 == word)
                {
                    return words[i].Item2;
                }
            }

            return -1;
        }

        private bool WordAlreadyExists (string word, List<(string, int)> words)
        {
            foreach (var wrd in words)
            {
                if (wrd.Item1 == word)
                {
                    return true;
                }
            }

            return false;
        }         

        private void SplitTextBySeparators()
        {
            int prevSeparatorIndex = -1;

            for (var i = 0; i < text.Length; ++i)
            {
                if (text[i] == '\'')
                {
                    var pairedQuotationMarkIndex = FindPairedQuotationMarkIndex(i);
                    
                    splitedText.Add("\'");
                    splitedText.Add(text.Substring(i + 1, pairedQuotationMarkIndex - i - 1));
                    splitedText.Add("\'");
                    prevSeparatorIndex = pairedQuotationMarkIndex;
                    i = pairedQuotationMarkIndex + 1;
                }
                else if (separators.Contains(text[i]))
                {
                    if (i - prevSeparatorIndex > 1)
                    {
                        splitedText.Add(text.Substring(prevSeparatorIndex + 1, i - prevSeparatorIndex - 1));
                    }
                    splitedText.Add(text[i].ToString());
                    prevSeparatorIndex = i;
                }
            }
        }

        private int FindPairedQuotationMarkIndex(int quotationMarkIndex)
        {
            for (var i = quotationMarkIndex + 1; i < text.Length; ++i)
            {
                if (text[i] == '\'')
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
