using System.IO;
using FileMetadata.Mp3;
using NUnit.Framework;

namespace FileMetadata.Tests
{
    public static class Id3InfoReaderTests
    {
        public class ReadInfoByPattern
        {
            private const string FileBeginning = "irrelevant values go here";
            private const string RestOfTheContents = "rest of the contents goes here";

            [Test]
            public void GivenTextReader_WhenTitleExists_ItIsFound()
            {
                string testTitle = nameof(GivenTextReader_WhenTitleExists_ItIsFound);
                var reader = new StringReader(FileBeginning +
                                              $"{Mp3InfoReader.TitleIdSearchPattern}" +
                                              "      " +
                                              $"{SpecialChars.Etx}{testTitle}{SpecialChars.NullChar}" + 
                                              RestOfTheContents);


                string valueRead = Id3InfoReader.ReadInfoByPattern(reader, Mp3InfoReader.TitleIdSearchPattern);
                Assert.AreEqual(testTitle, valueRead);
            }

            [Test]
            public void GivenTextReader_WhenTitleIsNotThere_ItIsNotFound()
            {
                var reader = new StringReader(FileBeginning + 
                                              SpecialChars.NullChar +
                                              SpecialChars.NullChar +
                                              SpecialChars.NullChar +
                                              SpecialChars.Etx +
                                              RestOfTheContents);

                string valueRead = Id3InfoReader.ReadInfoByPattern(reader, Mp3InfoReader.TitleIdSearchPattern);
                Assert.AreEqual(string.Empty, valueRead);
            }

            [Test]
            public void GivenTextReader_WhenItIsEmpty_TitleIsNotFound()
            {
                var reader = new StringReader(string.Empty);
                string valueRead = Id3InfoReader.ReadInfoByPattern(reader, Mp3InfoReader.TitleIdSearchPattern);
                Assert.AreEqual(string.Empty, valueRead);
            }
        }
    }
}