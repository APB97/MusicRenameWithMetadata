using UnityEngine;

public class AppManagerReferencer : MonoBehaviour
{
    public void ShowConfirmQuit() => ApplicationManager.Instance.ShowConfirmQuit();
}
