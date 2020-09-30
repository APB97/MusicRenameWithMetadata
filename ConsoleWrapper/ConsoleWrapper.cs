using System;

namespace Rename.Helpers
{
    public class ConsoleWrapper
    {
        private bool Silent { get; set; }

        public void WriteLine()
        {
            if (Silent) return;
            Console.WriteLine();
        }

        public void WriteLine(string text)
        {
            if (Silent) return;
            Console.WriteLine(text);
        }

        public void Clear()
        {
            Console.Clear();
        }

        public void WriteLine(object value)
        {
            if (Silent) return;
            Console.WriteLine(value);
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