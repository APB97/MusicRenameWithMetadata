using System.Collections.Generic;

namespace CommandClassInterface
{
    public interface ICommandClass
    {
        IEnumerable<string> CommandsForJson { get; }
    }
}