using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ParametersCreatorBehaviour : MonoBehaviour
{
    [SerializeField] private Transform layout;
    [SerializeField] private GameObject template;

    public void AddParam(string parameter = null)
    {
        GameObject instance = Instantiate(template, layout);
        instance.SetActive(true);
        if (parameter == null) return;
        var allTexts = instance.GetComponentsInChildren<InputField>();
        allTexts.Last().text = parameter;
    }

    public void ClearParams()
    {
        for (int i = layout.childCount - 1; i >= 0; i--)
        {
            var gameObj = layout.GetChild(i).gameObject;
            if (gameObj.activeSelf)
                Destroy(gameObj);
        }
    }
}