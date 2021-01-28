using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace StringProcessor.SkipCommonWords.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void CommonWordsAreRemoved()
        {
            SkipCommonWordsProcessor processor = new SkipCommonWordsProcessor
            {
                CommonWords = new HashSet<string> {"music", "file"},
                SkipAbleChars = new HashSet<char> {'(', '[', ']', ')'}
            };

            string result = processor.Process(string.Join(" ", string.Join(" ", processor.CommonWords), "custom"));

            Assert.True(processor.CommonWords.Select(word => result.Contains(word)).All(b => b == false));
        }

        [Test]
        public void SkipAbleCharsAreSkipped()
        {
            SkipCommonWordsProcessor processor = new SkipCommonWordsProcessor
            {
                CommonWords = new HashSet<string>(),
                SkipAbleChars = new HashSet<char> {'(', '[', ']', ')'}
            };

            string result = processor.Process($"_{string.Join(string.Empty, processor.SkipAbleChars)}_");
            
            Assert.AreEqual("__", result);
        }
    }
}