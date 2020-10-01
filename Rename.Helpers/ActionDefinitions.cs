using System;
using System.Diagnostics.CodeAnalysis;

namespace Rename.Helpers
{
    [Serializable]
    [SuppressMessage("ReSharper", "UnassignedField.Global")]
    public struct ActionDefinitions
    {
        public ActionDefinition[] Actions;
    }
}