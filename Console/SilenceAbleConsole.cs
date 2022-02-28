using System.Collections.Generic;

namespace Console
{
    /// <inheritdoc cref="ISilenceAbleConsole"/>
    public class SilenceAbleConsole : ISilenceAbleConsole
    {
        public bool Silent { get; private set; }

        /// <inheritdoc />
        public IEnumerable<string> CommandsForJson { get; }

        /// <inheritdoc />
        public string GetHelpFor(string command)
        {
            return Console.ResourceManager.GetString($"{nameof(SilenceAbleConsole)}_{command}Help");
        }

        /// <summary>
        /// Create new instance of SilenceAbleConsole class
        /// </summary>
        public SilenceAbleConsole()
        {
            CommandsForJson = new []
            {
                nameof(BeSilent),
                nameof(DontBeSilent)
            };
        }

        /// <inheritdoc />
        public void WriteLine(string text)
        {
            if (Silent) return;
            System.Console.WriteLine(text);
        }

        /// <inheritdoc />
        public void Clear()
        {
            if (Silent) return;
            System.Console.Clear();
        }

        /// <inheritdoc />
        public void WriteLine(object value)
        {
            if (Silent) return;
            System.Console.WriteLine(value);
        }

        /// <inheritdoc />
        public void BeSilent()
        {
            Silent = true;
        }

        /// <inheritdoc />
        public void DontBeSilent()
        {
            Silent = false;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return nameof(Console);
        }
    }
}