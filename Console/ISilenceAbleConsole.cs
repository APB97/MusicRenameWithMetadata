using CommandClassInterface;

namespace Console
{
    /// <inheritdoc cref="IConsole" />
    /// in addition, it allows to silence the Console
    public interface ISilenceAbleConsole : ISilenceAble, IConsole, ICommandClass { }
}