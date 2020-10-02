using System;
using System.Collections.Generic;
using CommandClassInterface;
using Console;

namespace Rename.Helpers
{
    public abstract class SelectorBase : ICommandClass
    {
        protected readonly IConsole ConsoleWrapper;

        protected SelectorBase(IConsole consoleWrapper)
        {
            ConsoleWrapper = consoleWrapper ?? throw new ArgumentNullException(nameof(consoleWrapper));
        }

        protected abstract HashSet<string> Commands { get; }

        public void Help(string[] forCommands)
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
            var typeName = GetType().Name;
            foreach (string command in forCommands)
            {
                string helpForCommand = Rename_Helpers_Commands.ResourceManager.GetString($"{typeName}_{command}Help");
                // ReSharper disable once LocalizableElement
                System.Console.WriteLine("{0, 16}\t{1}", command, helpForCommand);
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

        public abstract IEnumerable<string> CommandsForJson { get; }
        
        public string GetHelpFor(string command)
        {
            return Rename_Helpers_Commands.ResourceManager.GetString($"{GetType().Name}_{command}Help");
        }
    }
}