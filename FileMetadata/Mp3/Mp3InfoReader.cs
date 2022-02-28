using System.IO;

namespace FileMetadata.Mp3
{
    /// <summary>
    /// Static class for reading Properties of *.mp3 file
    /// </summary>
    public static class Mp3InfoReader
    {
        private const string IdFormat = "{0}{1}{1}{1}";
        /// <summary>
        /// Title ID
        /// </summary>
        public static readonly string TitleIdSearchPattern = string.Format(IdFormat, "TIT2", SpecialChars.NullChar);
        /// <summary>
        /// Artists ID
        /// </summary>
        public static readonly string ArtistsIdSearchPattern = string.Format(IdFormat, "TPE1", SpecialChars.NullChar);
        /// <summary>
        /// Album ID
        /// </summary>
        public static readonly string AlbumIdSearchPattern = string.Format(IdFormat, "TALB", SpecialChars.NullChar);

        /// <summary>
        /// Get Title of given file, if any. 
        /// </summary>
        /// <param name="fileAtPath">Path to file we want to get title from</param>
        /// <returns>Returns Title from file properties or string.Empty</returns>
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
        
        /// <summary>
        /// Get Album name of given file, if any.
        /// </summary>
        /// <param name="fileAtPath">Path to file we want to get album name from</param>
        /// <returns>Returns Album name from file properties or string.Empty</returns>
        public static string Album(string fileAtPath)
        {
            return ReadPropertyFromFile(fileAtPath, AlbumIdSearchPattern);
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