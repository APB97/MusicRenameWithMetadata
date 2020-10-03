namespace StringProcessor.DefaultNone
{
    /// <summary>
    /// Null object pattern for IStringProcessor
    /// </summary>
    public class DefaultNoProcessor : IStringProcessor
    {
        /// <summary>
        /// Return parameter as-is
        /// </summary>
        /// <param name="text"><inheritdoc cref="IStringProcessor.Process"/></param>
        /// <returns>Returns input parameter</returns>
        public string Process(string text) => text;

        /// <summary>
        /// Default instance of null object
        /// </summary>
        public static IStringProcessor Instance { get; } = new DefaultNoProcessor();
    }
}