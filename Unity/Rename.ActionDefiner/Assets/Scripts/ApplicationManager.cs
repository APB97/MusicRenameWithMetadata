using UnityEngine;

public class ApplicationManager : MonoBehaviour
{
    [SerializeField] private GameObject confirmPanel;
    private bool _wantsToQuit = false;

    public static ApplicationManager Instance { get; private set; }
    
    public void ExitApplication()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif
    }

    private void OnEnable()
    {
        Application.wantsToQuit += OnWantsToQuit;
        Instance = this;
    }

    private void OnDisable()
    {
        Application.wantsToQuit -= OnWantsToQuit;
        Instance = null;
    }

    private bool OnWantsToQuit()
    {
        if (!confirmPanel) return true;
        confirmPanel.SetActive(true);
        return _wantsToQuit;
    }

    public void SetThatWantsToQuit()
    {
        _wantsToQuit = true;
    }

    public void ShowConfirmQuit()
    {
        confirmPanel.SetActive(true);
    }
}