using System;
using System.Collections.Generic;

namespace MusicMetadataRenamer
{
    public abstract class SelectorBase
    {
        protected readonly ConsoleWrapper ConsoleWrapper;

        protected SelectorBase(ConsoleWrapper consoleWrapper)
        {
            ConsoleWrapper = consoleWrapper;
        }

        protected abstract HashSet<string> Commands { get; }

        public abstract void Clear();
        
        public virtual void Help()
        {
            foreach (string command in Commands)
            {
                Console.WriteLine(command);
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