using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMetadataRenamer
{
    public class WordSkipping : ICommonWords
    {
        private HashSet<string> _commonWords;

        public HashSet<string> CommonWords => _commonWords;

        public async Task GetCommonWordsFrom(string file)
        {
            var words = await File.ReadAllLinesAsync(file);
            
            var lowercaseWords = words.Select(word => new string(word.Select(char.ToLower).ToArray())).ToArray();
            var pascalCase = lowercaseWords.Select(word =>
            {
                char[] chars = word.ToCharArray();
                chars[0] = char.ToUpper(chars[0]);
                return new string(chars);
            });
            
            var combined = lowercaseWords.Concat(pascalCase);
            _commonWords = new HashSet<string>(combined);
        }
    }
}