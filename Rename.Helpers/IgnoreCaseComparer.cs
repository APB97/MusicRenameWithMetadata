using System;
using System.Collections.Generic;

namespace Rename.Helpers
{
    /// <inheritdoc />
    public class IgnoreCaseComparer : IEqualityComparer<string>
    {
        /// <inheritdoc />
        public bool Equals(string x, string y)
        {
            return string.Equals(x, y, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <inheritdoc />
        public int GetHashCode(string obj)
        {
            return obj.ToLowerInvariant().GetHashCode();
        }
    }
}