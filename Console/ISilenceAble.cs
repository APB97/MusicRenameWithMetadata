namespace Console
{
    /// <summary>
    /// Interface for suppressing output
    /// </summary>
    public interface ISilenceAble
    {
        bool Silent { get; }

        /// <summary>
        /// Implement in class to disable output when called
        /// </summary>
        void BeSilent();

        /// <summary>
        /// Implement in class to enable output when called
        /// </summary>
        void DontBeSilent();
    }
}