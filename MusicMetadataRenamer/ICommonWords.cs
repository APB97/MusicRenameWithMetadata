using System.Collections.Generic;

namespace MusicMetadataRenamer
{
    public interface ICommonWords
    {
        HashSet<string> CommonWords { get; }
    }
}