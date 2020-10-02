using System;
using System.IO;

namespace FileMetadata.Mp3
{
    public static class Mp3InfoReader
    {
        private const string TitleIdSearchPattern = "\0TIT2\0\0\0";
        private const int JitterBetween = 4;

        public static string TitleOf(string fileAtPath)
        {
            // using declarations
            using FileStream stream = File.OpenRead(fileAtPath);
            using StreamReader reader = new StreamReader(stream);

            // Initial values
            string cumulativeString = string.Empty;
            int searchStart = 0;
            
            while (!reader.EndOfStream)
            {
                cumulativeString += reader.ReadLine();
                int indexOfTitleId = cumulativeString.IndexOf(TitleIdSearchPattern, searchStart, StringComparison.Ordinal);
                if (indexOfTitleId > 0)
                {
                    // found Title
                    int titleValueIndexStart = indexOfTitleId + TitleIdSearchPattern.Length + JitterBetween;
                    int titleValueIndexEnd = cumulativeString.IndexOf((char) 0x0, titleValueIndexStart);
                    string title = cumulativeString.Substring(titleValueIndexStart, titleValueIndexEnd - titleValueIndexStart);
                    return title;
                }
                
                // Simplify next search by setting new searchStart
                searchStart = cumulativeString.Length - TitleIdSearchPattern.Length;
            }

            // Title not found 
            return string.Empty;
        }
    }
}