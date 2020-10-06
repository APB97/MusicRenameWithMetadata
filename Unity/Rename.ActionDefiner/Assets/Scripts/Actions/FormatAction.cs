using System.Collections.Generic;

namespace Actions
{
    public static class FormatAction
    {
        public static string ActionToString(string actionClassText, string actionNameText,
            IEnumerable<string> parameters)
        {
            return parameters != null
                ? $"{actionClassText}.{actionNameText} {string.Join(" ", parameters)}"
                : $"{actionClassText}.{actionNameText}";
        }
    }
}