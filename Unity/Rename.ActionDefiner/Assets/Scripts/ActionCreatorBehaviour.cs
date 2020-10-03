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
    [SerializeField] private Button buttonRemove;
    
    private ActionsCreator _creator;
    private int? _selectedIndex;
    private Button _button;
    private ParametersCreatorBehaviour _parametersCreator;

    private void Awake()
    {
        _creator = new ActionsCreator(new List<ActionDefinition>());
        _parametersCreator = GetComponent<ParametersCreatorBehaviour>();
    }

    public void Add()
    {
        var parameters = PopAllParameters();

        _creator.Add(actionClass.text, actionName.text, parameters.ToArray());

        var btn = Instantiate(buttonTemplate, actionsListingParent);
        btn.GetComponentInChildren<Text>().text = StringifyActionWithParams(parameters);
        btn.gameObject.SetActive(true);
        btn.onClick.AddListener(() =>
        {
            for (int index = 1; index < actionsListingParent.childCount; index++)
            {
                if (actionsListingParent.GetChild(index) != btn.transform) continue;
                SelectForUpdate(index - 1, btn);
                return;
            }
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

        var definition = _creator.ElementAt(indexOfAdded);
        actionClass.text = definition.ActionClass;
        Dropdown dropdownClass = actionClass.GetComponentInParent<Dropdown>();
        dropdownClass.onValueChanged.Invoke(dropdownClass.options.FindIndex(data => data.text == definition.ActionClass));
        
        actionName.text = definition.ActionName;
        Dropdown dropdownName = actionName.GetComponentInParent<Dropdown>();
        dropdownName.onValueChanged.Invoke(dropdownName.options.FindIndex(data => data.text == definition.ActionName));
        
        _parametersCreator.ClearParams();
        if (definition.ActionParameters != null)
            foreach (string parameter in definition.ActionParameters)
                _parametersCreator.AddParam(parameter);
        
        ShowButtonsForSelection();
    }

    public void CreateFile()
    {
        _creator.CreateFile(path.text);
    }

    public void UpdateCurrentElement()
    {
        var parameters = PopAllParameters();
        _creator.UpdateAt(_selectedIndex, actionClass.text, actionName.text, parameters.ToArray());
        _button.GetComponentInChildren<Text>().text = StringifyActionWithParams(parameters);
        ShowButtonsForSelection(false);
        _parametersCreator.ClearParams();
    }

    private void ShowButtonsForSelection(bool show = true)
    {
        buttonUpdate.gameObject.SetActive(show);
        buttonRemove.gameObject.SetActive(show);
    }

    public void RemoveCurrentElement()
    {
        _creator.RemoveAt(_selectedIndex);
        Destroy(_button.gameObject);
        ShowButtonsForSelection(false);
        _parametersCreator.ClearParams();
    }
}