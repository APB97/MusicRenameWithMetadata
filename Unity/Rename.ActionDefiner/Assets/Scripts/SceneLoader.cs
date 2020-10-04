using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private UnityEvent onLoadCompleted;
    [SerializeField] private UnityEvent onUnloadCompleted;

    public void StartLoadOf(string sceneName)
    {
        if (SceneManager.GetSceneByName(sceneName).isLoaded) return;
        var operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        operation.allowSceneActivation = true;
        operation.completed += op => onLoadCompleted.Invoke();
        StartCoroutine(ExecuteLoadOperation(operation));
    }

    public void StartUnloadOf(string sceneName)
    {
        if (!SceneManager.GetSceneByName(sceneName).isLoaded) return;
        var operation = SceneManager.UnloadSceneAsync(sceneName);
        operation.completed += op => onUnloadCompleted.Invoke();
        StartCoroutine(ExecuteLoadOperation(operation));
    }
    
    private IEnumerator ExecuteLoadOperation(AsyncOperation asyncOperation)
    {
        yield return asyncOperation;
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}