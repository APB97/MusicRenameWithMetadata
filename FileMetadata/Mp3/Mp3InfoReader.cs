using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace FileMetadata.Mp3
{
    [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Used via Reflection")]
    public static class Mp3InfoReader
    {
        private const string TitleIdSearchPattern = "\0TIT2\0\0\0";
        private const string ArtistsIdSearchPattern = "\0TPE1\0\0\0";

        public static string Title(string fileAtPath)
        {
            // using declarations
            using FileStream stream = File.OpenRead(fileAtPath);
            using StreamReader reader = new StreamReader(stream);

            return ReadInfoByPattern(reader, TitleIdSearchPattern);
        }

        public static string Artists(string fileAtPath)
        {
            // using declarations
            using FileStream stream = File.OpenRead(fileAtPath);
            using StreamReader reader = new StreamReader(stream);

            return ReadInfoByPattern(reader, ArtistsIdSearchPattern);
        }

        private static string ReadInfoByPattern(StreamReader reader, string idSearchPattern)
        {
            // Initial values
            string cumulativeString = string.Empty;
            int searchBegin = 0;

            while (!reader.EndOfStream)
            {
                cumulativeString += reader.ReadLine();
                int indexOfId = cumulativeString.IndexOf(idSearchPattern, searchBegin, StringComparison.Ordinal);
                if (indexOfId > 0)
                {
                    // found Id // ETX char = 0x03
                    int valueIndexBegin = cumulativeString.IndexOf((char)0x03, indexOfId + idSearchPattern.Length);
                    if (valueIndexBegin >= cumulativeString.Length || valueIndexBegin < 0)
                        continue;
                    int valueIndexEnd = cumulativeString.IndexOf((char) 0x0, valueIndexBegin);
                    string valueRead = cumulativeString.Substring(valueIndexBegin + 1, valueIndexEnd - valueIndexBegin);
                    return valueRead;
                }

                // Simplify next search by setting new searchBegin
                searchBegin = cumulativeString.Length - idSearchPattern.Length;
            }

            // Not found
            return string.Empty;
        }
    }
}