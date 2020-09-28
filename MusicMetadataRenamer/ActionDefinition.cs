using System;

namespace MusicMetadataRenamer
{
    [Serializable]
    public struct ActionDefinition
    {
        public string ActionClass;
        public string ActionName;
        public string[] ActionParameters;
    }
}