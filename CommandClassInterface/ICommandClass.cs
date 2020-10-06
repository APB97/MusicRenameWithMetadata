using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CommandClassInterface
{
    /// <summary>
    /// Interface used to implement classes containing Commands (available for JSON)
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global", Justification = "Used in Unity project")]
    [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Used in Unity project")]
    public interface ICommandClass
    {
        /// <summary>
        /// Collection of commands available to use from JSON ActionDefinitions file
        /// </summary>
        IEnumerable<string> CommandsForJson { get; }
        /// <summary>
        /// Get Help message for given command
        /// </summary>
        /// <param name="command">Command which help message we want to obtain</param>
        /// <returns>Returns help for given command</returns>
        string GetHelpFor(string command);
    }
}