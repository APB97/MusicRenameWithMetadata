using System;
using System.Collections.Generic;

namespace MusicMetadataRenamer
{
    public abstract class SelectorBase
    {
        protected abstract HashSet<string> Commands { get; }

        public virtual void Help()
        {
            foreach (string command in Commands)
            {
                Console.WriteLine(command);
            }
        }

        public void ClearScreen()
        {
            Console.Clear();
        }

        public bool Complete()
        {
            return true;
        }
    }
}