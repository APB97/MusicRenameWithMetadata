using System.IO;
using System.Linq;

namespace FileMetadata.Extensions
{
    /// <summary>
    /// Static class containing Extension method(s) for string(s).
    /// </summary>
    public static class StringExtensions
    {
        private static readonly char[] InvalidChars = Path.GetInvalidFileNameChars();
        
        /// <summary>
        /// Extension method to join strings with separator but skipping invalid filename chars.
        /// </summary>
        /// <param name="joinThese">Array of strings to join.</param>
        /// <param name="separator">Separator to be inserted between strings.</param>
        /// <returns>Joined strings for usage as a filename.</returns>
        public static string JoinForFilePath(this string[] joinThese, string separator = " - ")
        {
            return new string(string.Join(separator, joinThese.Where(s => !string.IsNullOrEmpty(s)))
                .Where(c => !InvalidChars.Contains(c)).ToArray());
        }
    }
}