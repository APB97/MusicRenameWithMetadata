﻿using System.Collections.Generic;

namespace CommandClassInterface
{
    /// <summary>
    /// Interface used to implement classes containing Commands (available for JSON)
    /// </summary>
    public interface ICommandClass
    {
        /// <summary>
        /// Collection of commands available to use from JSON ActionDefinitions file
        /// </summary>
        IEnumerable<string> CommandsForJson { get; }

        /// <summary>
        /// Get Help message for given command
        /// </summary>
        /// <param name="command">Command for which we want to obtain a help message</param>
        /// <returns>Returns help for given command</returns>
        string GetHelpFor(string command);
    }
}