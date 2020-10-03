namespace StringProcessor
{
    /// <summary>
    /// Interface for string processing.
    /// </summary>
    public interface IStringProcessor
    {
        /// <summary>
        /// Processes given text. It can be a simple return or return skipping certain characters
        /// </summary>
        /// <param name="text">Text to process</param>
        /// <returns>Processed string</returns>
        string Process(string text);
    }
}