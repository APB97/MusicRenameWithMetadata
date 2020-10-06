using UnityEngine;

public class ApplicationManager : MonoBehaviour
{
    [SerializeField] private GameObject confirmPanel;
    private static GameObject _confirmPanelInstance;

    private void Awake()
    {
        _confirmPanelInstance = confirmPanel;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    static void RunOnStart()
    {
        Application.wantsToQuit += () =>
        {
            if (!_confirmPanelInstance) return true;
            _confirmPanelInstance.SetActive(true);
            return AppExit.WantsToExit;
        };
    }

    public void ShowConfirmQuit()
    {
        confirmPanel.SetActive(true);
    }
}