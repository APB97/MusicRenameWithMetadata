using UnityEngine;
using UnityEngine.Events;

public class EnabledStateHooks : MonoBehaviour
{
    [SerializeField] private UnityEvent onEnabledEvent;
    [SerializeField] private UnityEvent onDisabledEvent;
    
    private void OnEnable()
    {
        onEnabledEvent.Invoke();
    }

    private void OnDisable()
    {
        onDisabledEvent.Invoke();
    }
}