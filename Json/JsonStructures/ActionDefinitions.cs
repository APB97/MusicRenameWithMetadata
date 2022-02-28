using System;

namespace JsonStructures
{
    /// <summary>
    /// Possibly multiple Action Definitions are stored in JSON using this structure.
    /// </summary>
    [Serializable]
    public struct ActionDefinitions
    {
        /// <summary>
        /// All setup actions to perform
        /// </summary>
        public ActionDefinition[] Actions;
    }
}