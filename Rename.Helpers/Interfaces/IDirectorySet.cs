using System.Collections.Generic;

namespace Rename.Helpers
{
    public interface IDirectorySet
    {
        HashSet<string> Directories { get; }
    }
}