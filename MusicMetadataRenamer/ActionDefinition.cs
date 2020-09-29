﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace MusicMetadataRenamer
{
    [Serializable]
    [SuppressMessage("ReSharper", "UnassignedField.Global")]
    public struct ActionDefinition
    {
        public string ActionClass;
        public string ActionName;
        public string[] ActionParameters;
    }
}