using UnityEngine;

public class AppExit : MonoBehaviour
{
    public void ExitApplicationForEvents() => ExitApplication();

    private static void ExitApplication()    
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
    }

    public void SetThatWantsToExit() => WantsToExit = true;
    
    public static bool WantsToExit { get; private set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    private static void RunOnStart()
    {
        Application.wantsToQuit += () => WantsToExit;
    }
}