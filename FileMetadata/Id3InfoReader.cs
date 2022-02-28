using System;
using System.IO;
using System.Linq;

namespace FileMetadata
{
    /// <summary>
    /// Static class used to read property info from text reader.
    /// </summary>
    public static class Id3InfoReader
    {
        /// <summary>
        /// Attempts to read value of a property from id3 container.
        /// </summary>
        /// <param name="reader">Reader to get text from.</param>
        /// <param name="idSearchPattern">pattern used to search id and its value.</param>
        /// <returns>Returns property value if found, string.Empty otherwise.</returns>
        public static string ReadInfoByPattern(TextReader reader, string idSearchPattern)
        {
            if (reader == null)
                return string.Empty;
            
            // Initial values
            string line;
            string cumulativeString = string.Empty;
            int searchBegin = 0;

            do
            {
                line = reader.ReadLine();
                cumulativeString += line;
                var (valueRead, newSearchBegin) = SearchForValue(idSearchPattern, cumulativeString, searchBegin);
                if (valueRead != null) return valueRead;
                searchBegin = newSearchBegin;
            } while (line != null && line.LastOrDefault() != SpecialChars.Bell);

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
            var valueIndexBegin = GetValueIndexBegin(cumulativeString, searchStart, out int valueLength);
            if (valueIndexBegin >= cumulativeString.Length || valueIndexBegin <= 0)
                return null;
            if (valueLength >= 0)
            {
                return cumulativeString.Substring(valueIndexBegin, valueLength);
            }
            return cumulativeString.Substring(valueIndexBegin,
                cumulativeString.IndexOf(SpecialChars.NullChar, valueIndexBegin) - valueIndexBegin);
        }

        private static int GetValueIndexBegin(string cumulativeString, int startIndex, out int valueLength)
        {
            int etx;
            valueLength = -1;
            if (startIndex < cumulativeString.Length - 2 && cumulativeString[startIndex + 1] == SpecialChars.NullChar)
            {
                valueLength = cumulativeString[startIndex] - 1;
                return startIndex + 4;
            }
            else
            {
                etx = cumulativeString.IndexOf(SpecialChars.Etx, startIndex) + 1;
            }

            if (etx < 0 || etx >= cumulativeString.Length)
            {
                etx = cumulativeString.IndexOf(SpecialChars.NullChar, startIndex) - 4;
            }
            return etx;
        }
    }
}