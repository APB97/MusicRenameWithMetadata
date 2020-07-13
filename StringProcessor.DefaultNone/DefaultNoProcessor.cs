namespace StringProcessor.DefaultNone
{
    public class DefaultNoProcessor : IStringProcessor
    {
        public string Process(string text) => text;

        public static IStringProcessor Instance { get; } = new DefaultNoProcessor();
    }
}