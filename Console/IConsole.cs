namespace Console
{
    public interface IConsole : ISilenceAble
    {
        void WriteLine();
        void WriteLine(string text);
        void Clear();
        void WriteLine(object value);
    }
}