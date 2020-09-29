using System.Collections.Generic;

namespace MusicMetadataRenamer
{
    public interface IDirectorySet
    {
        HashSet<string> Directories { get; }
    }
}