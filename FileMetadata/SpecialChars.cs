namespace FileMetadata
{
    /// <summary>
    /// Special characters used to find property value by id
    /// </summary>
    public static class SpecialChars
    {
        /// <summary>
        /// End of text character.
        /// </summary>
        public const char Etx = (char) 0x03;

        /// <summary>
        /// Null, '\0', character.
        /// </summary>
        public const char NullChar = (char) 0x0;

        /// <summary>
        /// Bell character.
        /// </summary>
        public const char Bell = (char) 0x07;
    }
}