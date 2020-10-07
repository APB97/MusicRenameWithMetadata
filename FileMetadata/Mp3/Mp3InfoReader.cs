using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace FileMetadata.Mp3
{
    /// <summary>
    /// Static class for reading Properties of *.mp3 file
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Used via Reflection")]
    public static class Mp3InfoReader
    {
        private const string IdFormat = "{1}{0}{1}{1}{1}";
        public static readonly string TitleIdSearchPattern = string.Format(IdFormat, "TIT2", SpecialChars.NullChar);
        public static readonly string ArtistsIdSearchPattern = string.Format(IdFormat, "TPE1", SpecialChars.NullChar);

        /// <summary>
        /// Get Title of given file, if any. 
        /// </summary>
        /// <param name="fileAtPath">Path to file we want to get title from</param>
        /// <returns>Returns Title from file properties or string.Empty</returns>
        [SuppressMessage("ReSharper", "ConvertToUsingDeclaration")]
        public static string Title(string fileAtPath)
        {
            return ReadPropertyFromFile(fileAtPath, TitleIdSearchPattern);
        }

        /// <summary>
        /// Get Artists of given file, if any.
        /// </summary>
        /// <param name="fileAtPath">Path to file we want to get artists from</param>
        /// <returns>Returns Artists from file properties or string.Empty</returns>
        public static string Artists(string fileAtPath)
        {
            return ReadPropertyFromFile(fileAtPath, ArtistsIdSearchPattern);
        }

        private static string ReadPropertyFromFile(string fileAtPath, string propertyIdPattern)
        {
            // using declarations
            using (FileStream stream = File.OpenRead(fileAtPath))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return Id3InfoReader.ReadInfoByPattern(reader, propertyIdPattern);
                }
            }
        }
    }
}