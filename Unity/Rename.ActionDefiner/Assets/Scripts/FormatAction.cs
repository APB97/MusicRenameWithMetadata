using System.Collections.Generic;

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