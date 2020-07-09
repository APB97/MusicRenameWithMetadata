using System.IO;
using System.Linq;

namespace FileMetadata.Extensions
{
    public static class StringExtensions
    {
        private static readonly char[] InvalidChars = Path.GetInvalidFileNameChars();
        
        public static string JoinForFilePath(this string[] joinThese, string separator = " - ")
        {
            return new string(string.Join(separator, joinThese).Where(c => !InvalidChars.Contains(c)).ToArray());
        }
    }
}