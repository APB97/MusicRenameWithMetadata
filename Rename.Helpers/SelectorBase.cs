using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommandClassInterface;
using Console;

namespace Rename.Helpers
{
    /// <summary>
    /// Abstract base class for selector classes.
    /// </summary>
    public abstract class SelectorBase : ICommandClass, ISupportsInteractiveMode
    {
        /// <summary>
        /// Target of commands' output
        /// </summary>
        protected readonly ISilenceAbleConsole SilenceAbleConsole;

        /// <summary>
        /// Called in inherited classes during creation of new instance to set up Silence Able Console
        /// or throw exception if null.
        /// </summary>
        /// <param name="silenceAbleConsole">Silence Able Console used as output target. Cannot be null.</param>
        protected SelectorBase(ISilenceAbleConsole silenceAbleConsole)
        {
            SilenceAbleConsole = silenceAbleConsole ?? throw new ArgumentNullException(nameof(silenceAbleConsole));
        }

        /// <summary>
        /// Set of all unique Commands.
        /// </summary>
        protected abstract HashSet<string> Commands { get; }

        /// <summary>
        /// Display help for all commands or given command(s), if any.
        /// </summary>
        /// <param name="forCommands">Commands we want to get help for.</param>
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

        private void HelpInternal(IEnumerable<string> forCommands)
        {
            var typeName = GetType().Name;
            foreach (string command in forCommands)
            {
                string helpForCommand = Rename_Helpers_Commands.ResourceManager.GetString($"{typeName}_{command}Help");
                // ReSharper disable once LocalizableElement
                System.Console.WriteLine("{0, -16}\t{1}", command, helpForCommand);
            }
        }

        /// <summary>
        /// Clear Console screen.
        /// </summary>
        public void ClearScreen()
        {
            SilenceAbleConsole.Clear();
        }


        /// <summary>
        /// Complete this step of interactive mode.
        /// </summary>
        public void Complete() { }

        /// <summary>
        /// Collection of commands exposed for use through a JSON file.
        /// </summary>
        public abstract IEnumerable<string> CommandsForJson { get; }
        
        /// <summary>
        /// Get help text for command of current class.
        /// </summary>
        /// <param name="command">Command to get help for.</param>
        /// <returns>Text with command's help.</returns>
        public string GetHelpFor(string command)
        {
            return Rename_Helpers_Commands.ResourceManager.GetString($"{GetType().Name}_{command}Help");
        }

        /// <inheritdoc />
        public void StartInteractive()
        {
            while (true)
            {
                if (!ProcessInputs()) return;
            }
        }

        /// <summary>
        /// Processes user inputs.
        /// </summary>
        /// <returns>If user decided to Complete the step, returns false. Returns true otherwise.</returns>
        private bool ProcessInputs()
        {
            System.Console.WriteLine(Rename_Helpers_Commands.Type_Help_for_help, nameof(DirectorySelector));

            string line = System.Console.ReadLine();
            string[] inputs = line?.Split(' ');
            string command = inputs?[0];

            if (string.IsNullOrWhiteSpace(command) || !Commands.Contains(command))
                return true;
            if (command.Equals(nameof(Complete)))
                return false;

            MethodInfo methodInfo = GetType().GetMethod(command);
            methodInfo?.Invoke(this,
                methodInfo.GetParameters().Length == 1
                    ? new object[] {inputs.Skip(1).ToArray()}
                    : new object[] { });
            
            return true;
        }
    }
}