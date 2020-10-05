using UnityEngine;

public static class AppManagerHelper
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    static void RunOnStart()
    {
        Application.wantsToQuit += () => AppExit.WantsToExit;
    }
}
