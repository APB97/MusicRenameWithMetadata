using System;
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
        private static readonly string TitleIdSearchPattern = string.Format(IdFormat, "TIT2", SpecialChars.NullChar);
        private static readonly string ArtistsIdSearchPattern = string.Format(IdFormat, "TPE1", SpecialChars.NullChar);

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
                    return ReadInfoByPattern(reader, propertyIdPattern);
                }
            }
        }

        private static string ReadInfoByPattern(StreamReader reader, string idSearchPattern)
        {
            // Initial values
            string cumulativeString = string.Empty;
            int searchBegin = 0;

            while (!reader.EndOfStream)
            {
                cumulativeString += reader.ReadLine();
                var (valueRead, newSearchBegin) = SearchForValue(idSearchPattern, cumulativeString, searchBegin);
                if (valueRead != null) return valueRead;
                searchBegin = newSearchBegin;
            }

            // Not found
            return string.Empty;
        }

        private static (string valueRead, int newSearchBegin) SearchForValue(string idSearchPattern, string cumulativeString, int searchBegin)
        {
            int indexOfId = cumulativeString.IndexOf(idSearchPattern, searchBegin, StringComparison.Ordinal);
            // found Id
            if (indexOfId > 0)
            {
                string valueRead = TryReadValue(cumulativeString, indexOfId + idSearchPattern.Length);
                if (valueRead != null)
                    return (valueRead, searchBegin);
            }
            // ID not found. Simplify next search by setting new searchBegin
            else
                return (null, cumulativeString.Length - idSearchPattern.Length);

            // Id found but index would be out of range
            return (null, searchBegin);
        }

        private static string TryReadValue(string cumulativeString, int searchStart)
        {
            var valueIndexBegin = GetValueIndexBegin(cumulativeString, searchStart);
            if (valueIndexBegin >= cumulativeString.Length || valueIndexBegin <= 0)
                return null;
            return cumulativeString.Substring(valueIndexBegin,
                cumulativeString.IndexOf(SpecialChars.NullChar, valueIndexBegin) - valueIndexBegin);
        }

        private static int GetValueIndexBegin(string cumulativeString, int startIndex)
        {
            return cumulativeString.IndexOf(SpecialChars.ETX, startIndex) + 1;
        }
    }
}