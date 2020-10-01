using System;
using System.Diagnostics.CodeAnalysis;

namespace MusicMetadataRenamer
{
    [Serializable]
    [SuppressMessage("ReSharper", "UnassignedField.Global")]
    public struct ActionDefinitions
    {
        public ActionDefinition[] Actions;
    }
}