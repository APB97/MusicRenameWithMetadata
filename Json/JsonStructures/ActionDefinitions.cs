using System;
using System.Diagnostics.CodeAnalysis;

namespace JsonStructures
{
    /// <summary>
    /// Possibly multiple Action Definitions are stored in JSON using this structure.
    /// </summary>
    [Serializable]
    [SuppressMessage("ReSharper", "UnassignedField.Global")]
    public struct ActionDefinitions
    {
        /// <summary>
        /// All setup actions to perform
        /// </summary>
        public ActionDefinition[] Actions;
    }
}