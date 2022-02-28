using CommandClassInterface;

namespace Console
{
    /// <summary>
    /// Based on <see cref="IConsole"/> interface but in addition, it allows to silence the Console
    /// </summary>
    public interface ISilenceAbleConsole : ISilenceAble, IConsole, ICommandClass { }
}