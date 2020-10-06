using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Rename.Helpers
{
    public class WordSkipping
    {
        public async Task<HashSet<string>> GetCommonWordsFrom(string file)
        {
            return await Task.Run(() => new HashSet<string>(File.ReadAllLines(file), new IgnoreCaseComparer()));
        }
    }
}