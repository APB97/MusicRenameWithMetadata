﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Rename.Helpers
{
    public class WordSkipping
    {
        public HashSet<string> CommonWords { get; private set; }

        public void GetCommonWordsFrom(string file)
        {
            var words = File.ReadAllLines(file);
            
            var lowercaseWords = words.Select(word => new string(word.Select(char.ToLower).ToArray())).ToArray();
            var pascalCase = lowercaseWords.Select(word =>
            {
                char[] chars = word.ToCharArray();
                chars[0] = char.ToUpper(chars[0]);
                return new string(chars);
            });
            
            var combined = lowercaseWords.Concat(pascalCase);
            CommonWords = new HashSet<string>(combined);
        }
    }
}