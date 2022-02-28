namespace Console
{
    /// <summary>
    /// Interface wrapping most basic Console functionality
    /// </summary>
    public interface IConsole
    {
        /// <summary>
        /// Write contents of string to Console, followed by current line terminator
        /// </summary>
        /// <param name="text">Text to display on Console</param>
        void WriteLine(string text);
        
        /// <summary>
        /// Clear Console's screen
        /// </summary>
        void Clear();
        
        /// <summary>
        /// Write string representation of an object followed by current line terminator
        /// </summary>
        /// <param name="value">Object to display on Console</param>
        void WriteLine(object value);
    }
}