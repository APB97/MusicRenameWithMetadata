using UnityEngine;

public class ApplicationManager : MonoBehaviour
{
    [SerializeField] private GameObject confirmPanel;

    public static ApplicationManager Instance { get; private set; }
    
    private void Awake()
    {
        Application.wantsToQuit += () =>
        {
            if (!confirmPanel) return true;
            confirmPanel.SetActive(true);
            return AppExit.WantsToExit;
        };
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    private static void RunOnStart()
    {
        GameObject managerPrefab = Resources.Load<GameObject>(nameof(ApplicationManager));
        var managerInstance = Instantiate(managerPrefab);
        DontDestroyOnLoad(managerInstance);
        Instance = managerInstance.GetComponent<ApplicationManager>();
    }

    public void ShowConfirmQuit()
    {
        confirmPanel.SetActive(true);
    }
}