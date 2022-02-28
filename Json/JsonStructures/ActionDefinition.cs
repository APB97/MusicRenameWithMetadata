using System;

namespace JsonStructures
{
    /// <summary>
    /// Singular Action Definition for JSON
    /// </summary>
    [Serializable]
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