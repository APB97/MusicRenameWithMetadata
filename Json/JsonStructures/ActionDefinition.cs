using System;
using System.Diagnostics.CodeAnalysis;

namespace JsonStructures
{
    /// <summary>
    /// Singular Action Definition for JSON
    /// </summary>
    [Serializable]
    [SuppressMessage("ReSharper", "UnassignedField.Global")]
    public struct ActionDefinition
    {
        /// <summary>
        /// Name of the class containing Action
        /// </summary>
        public string ActionClass;
        /// <summary>
        /// Name of the Action
        /// </summary>
        public string ActionName;
        /// <summary>
        /// Action's parameters, if any
        /// </summary>
        public string[] ActionParameters;
    }
}