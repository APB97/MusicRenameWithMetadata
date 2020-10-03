using System;
using System.Diagnostics.CodeAnalysis;

namespace JsonStructures
{
    [Serializable]
    [SuppressMessage("ReSharper", "UnassignedField.Global")]
    public struct ActionDefinitions
    {
        public ActionDefinition[] Actions;
    }
}