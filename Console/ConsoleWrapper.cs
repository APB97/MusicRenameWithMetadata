using System.Diagnostics.CodeAnalysis;

namespace Console
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class ConsoleWrapper : IConsole
    {
        private bool Silent { get; set; }

        public void WriteLine()
        {
            if (Silent) return;
            System.Console.WriteLine();
        }

        public void WriteLine(string text)
        {
            if (Silent) return;
            System.Console.WriteLine(text);
        }

        public void Clear()
        {
            if (Silent) return;
            System.Console.Clear();
        }

        public void WriteLine(object value)
        {
            if (Silent) return;
            System.Console.WriteLine(value);
        }
        
        public void BeSilent()
        {
            Silent = true;
        }

        public void DontBeSilent()
        {
            Silent = false;
        }
    }
}