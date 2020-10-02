using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using CommandClassInterface;

namespace Console
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class ConsoleWrapper : IConsole, ICommandClass
    {
        private bool Silent { get; set; }

        public IEnumerable<string> CommandsForJson { get; }
        public string GetHelpFor(string command)
        {
            return Console.ResourceManager.GetString($"{nameof(ConsoleWrapper)}_{command}Help");
        }

        public ConsoleWrapper()
        {
            CommandsForJson = new []
            {
                nameof(BeSilent),
                nameof(DontBeSilent)
            };
        }
        
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

        public override string ToString()
        {
            return nameof(Console);
        }
    }
}