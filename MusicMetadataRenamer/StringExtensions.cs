using System.IO;
using System.Linq;

namespace MusicMetadataRenamer
{
    public static class StringExtensions
    {
        private static char[] _illegals = Path.GetInvalidFileNameChars();
        
        public static string JoinForFilePath(this string[] joinThese, string separator = " - ")
        {
            return new string(string.Join(separator, joinThese).Where(c => !_illegals.Contains(c)).ToArray());
        }
    }
}