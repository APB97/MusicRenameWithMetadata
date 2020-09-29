using System;
using System.Collections.Generic;

namespace Rename.Helpers
{
    public abstract class SelectorBase
    {
        protected readonly ConsoleWrapper ConsoleWrapper;

        protected SelectorBase(ConsoleWrapper consoleWrapper)
        {
            ConsoleWrapper = consoleWrapper;
        }

        protected abstract HashSet<string> Commands { get; }
        
        protected abstract Dictionary<string, string> HelpDictionary { get; }
        
        public abstract void Clear();
        
        public virtual void Help(string[] forCommands)
        {
            if (forCommands.Length != 0)
            {
                HelpInternal(forCommands);
            }
            else
            {
                HelpInternal(Commands);
            }
        }

        protected void HelpInternal(IEnumerable<string> forCommands)
        {
            foreach (string command in forCommands)
            {
                Console.WriteLine(HelpDictionary.TryGetValue(command, out string helpText)
                    ? $"{command}\t{helpText}"
                    : command);
            }
        }

        public void ClearScreen()
        {
            ConsoleWrapper.Clear();
        }

        public bool Complete()
        {
            return true;
        }
    }
}