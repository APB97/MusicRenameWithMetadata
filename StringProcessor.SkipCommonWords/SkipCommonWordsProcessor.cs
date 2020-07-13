using System.Collections.Generic;
using System.Linq;

namespace StringProcessor.SkipCommonWords
{
    public class SkipCommonWordsProcessor : IStringProcessor
    {
        public HashSet<string> CommonWords { get; set; }

        public HashSet<char> SkipAbleChars { get; set; } = new HashSet<char>(new []{ '(', '[', ']', ')' });
        
        public string Process(string text)
        {
            char[] remainingChars = text.Where(c => !SkipAbleChars.Contains(c)).ToArray();
            string preProcessed = new string(remainingChars);
            string[] words = preProcessed.Split(' ');
            string processedResult = string.Join(' ', words.Where(word => !CommonWords.Contains(word)).ToArray());
            
            return processedResult;
        }
    }
}