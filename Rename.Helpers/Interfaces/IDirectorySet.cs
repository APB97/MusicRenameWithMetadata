using System.Collections.Generic;

namespace Rename.Helpers.Interfaces
{
    public interface IDirectorySet
    {
        HashSet<string> Directories { get; }
    }
}