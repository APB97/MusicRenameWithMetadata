using System.Collections.Generic;

namespace CommandClassInterface
{
    public interface ICommandClass
    {
        IReadOnlyDictionary<string, string> CommandsWithHelp { get; }
    }
}