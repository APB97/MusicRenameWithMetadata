using System.Collections.Generic;
using System.Linq;
using JsonStructures;
using UnityEngine;
using UnityEngine.UI;

public class ActionCreatorBehaviour : MonoBehaviour
{
    [SerializeField] private Text actionClass;
    [SerializeField] private Text actionName;
    [SerializeField] private InputField path;

    [SerializeField] private Transform paramsParent;
    [SerializeField] private Transform actionsListingParent;
    [SerializeField] private Button buttonTemplate;
    [SerializeField] private Button buttonUpdate;
    private ActionsCreator _creator;
    private int? _selectedIndex;
    private Button _button;

    private void Awake()
    {
        _creator = new ActionsCreator(new List<ActionDefinition>());
    }

    public void Add()
    {
        var parameters = PopAllParameters();

        int indexOfAdded = _creator.Add(actionClass.text, actionName.text, parameters.ToArray());

        var btn = Instantiate(buttonTemplate, actionsListingParent);
        btn.GetComponentInChildren<Text>().text = StringifyActionWithParams(parameters);
        btn.gameObject.SetActive(true);
        btn.onClick.AddListener(() =>
        {
            SelectForUpdate(indexOfAdded, btn);
        });
    }

    private string StringifyActionWithParams(List<string> parameters)
    {
        return $"{actionClass.text}.{actionName.text} {string.Join(" ", parameters)}";
    }

    private List<string> PopAllParameters()
    {
        List<string> parameters = new List<string>();

        for (int i = paramsParent.childCount - 1; i >= 0; i--)
        {
            if (paramsParent.GetChild(i).gameObject.activeSelf)
            {
                string text = paramsParent.GetChild(i).GetComponentsInChildren<Text>().Last().text;
                parameters.Add(text);
                Destroy(paramsParent.GetChild(i).gameObject);
            }
        }

        parameters.Reverse();
        return parameters;
    }

    private void SelectForUpdate(int indexOfAdded, Button button)
    {
        _selectedIndex = indexOfAdded;
        _button = button;
        buttonUpdate.gameObject.SetActive(true);
    }

    public void CreateFile()
    {
        _creator.CreateFile(path.text);
    }

    public void UpdateElement()
    {
        var parameters = PopAllParameters();
        _creator.UpdateAt(_selectedIndex, actionClass.text, actionName.text, parameters.ToArray());
        _button.GetComponentInChildren<Text>().text = StringifyActionWithParams(parameters);
        buttonUpdate.gameObject.SetActive(false);
    }
}