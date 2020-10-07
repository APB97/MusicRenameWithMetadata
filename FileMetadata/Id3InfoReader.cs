using System;
using System.IO;

namespace FileMetadata
{
    public static class Id3InfoReader
    {
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
            } while (line != null);

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