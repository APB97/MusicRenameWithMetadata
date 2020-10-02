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
            return TitleFrom(File.ReadAllText(fileAtPath));
        }

        private static string TitleFrom(string contents)
        {
            string contentsFirstN = contents.Substring(0, ushort.MaxValue);

            int titleIdIndex = contentsFirstN.IndexOf(TitleIdSearchPattern, StringComparison.Ordinal);
            int titleValueIndexStart = titleIdIndex + TitleIdSearchPattern.Length + JitterBetween;
            int titleValueIndexEnd = contentsFirstN.IndexOf((char) 0x0, titleValueIndexStart);

            string title = contentsFirstN.Substring(titleValueIndexStart, titleValueIndexEnd - titleValueIndexStart);
            return title;
        }
    }
}