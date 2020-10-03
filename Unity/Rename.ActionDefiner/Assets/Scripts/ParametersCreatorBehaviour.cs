using UnityEngine;

public class ParametersCreatorBehaviour : MonoBehaviour
{
    [SerializeField] private Transform layout;
    [SerializeField] private GameObject template;

    public void AddParam()
    {
        Instantiate(template, layout).SetActive(true);
    }
}