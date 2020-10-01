namespace Console
{
    public interface IConsole
    {
        void WriteLine();
        void WriteLine(string text);
        void Clear();
        void WriteLine(object value);
    }
}