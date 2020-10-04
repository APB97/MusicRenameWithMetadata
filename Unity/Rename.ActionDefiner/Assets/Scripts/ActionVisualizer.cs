using System.Collections.Generic;
using JsonStructures;
using UnityEngine;
using UnityEngine.UI;

public class ActionVisualizer : MonoBehaviour, ISelectedAction, IActionVisualizer
{
    [SerializeField] private Transform actionsListingParent;
    [SerializeField] private Button buttonTemplate;
    [SerializeField] private Button buttonUpdate;
    [SerializeField] private Button buttonRemove;
    private ActionCreatorBehaviour _actionCreatorBehaviour;
    private IActionsDropdowns _actionsDropdowns;
    
    private int _selectedIndex;
    private Button _button;

    public int SelectedIndex => _selectedIndex;

    private void Awake()
    {
        _actionCreatorBehaviour = GetComponent<ActionCreatorBehaviour>();
        _actionsDropdowns = GetComponent<IActionsDropdowns>();
    }

    public void AddVisualFor(ActionDefinition definition)
    {
        AddVisualFor(definition.ActionClass, definition.ActionName, definition.ActionParameters);
    }

    private void AddVisualFor(string actionClassText, string actionNameText, IEnumerable<string> parameters)
    {
        var btn = Instantiate(buttonTemplate, actionsListingParent);
        btn.GetComponentInChildren<Text>().text = FormatAction.ActionToString(actionClassText, actionNameText, parameters);
        btn.gameObject.SetActive(true);
        btn.onClick.AddListener(() =>
        {
            for (int index = 1; index < actionsListingParent.childCount; index++)
            {
                if (actionsListingParent.GetChild(index) != btn.transform) continue;
                SelectForUpdate(index - 1, btn, _actionCreatorBehaviour);
                ShowButtonsForSelection();
                return;
            }
        });
    }

    public void ShowButtonsForSelection(bool show = true)
    {
        buttonUpdate.gameObject.SetActive(show);
        buttonRemove.gameObject.SetActive(show);
    }

    public void SelectForUpdate(int indexOfAdded, Button button, ActionCreatorBehaviour actionCreatorBehaviour)
    {
        _selectedIndex = indexOfAdded;
        _button = button;

        var definition = actionCreatorBehaviour.Actions[indexOfAdded];
        _actionsDropdowns.DropdownClass.value = _actionsDropdowns.DropdownClass.options.FindIndex(data => data.text == definition.ActionClass);
        _actionsDropdowns.DropdownAction.value = _actionsDropdowns.DropdownAction.options.FindIndex(data => data.text == definition.ActionName);

        actionCreatorBehaviour.ParametersCreator.ClearParams();
        if (definition.ActionParameters == null) return;
        foreach (string parameter in definition.ActionParameters)
            actionCreatorBehaviour.ParametersCreator.AddParam(parameter);
    }

    public void UpdateActionVisual()
    {
        string actionClass = _actionsDropdowns.DropdownClass.options[_actionsDropdowns.DropdownClass.value].text;
        string actionName = _actionsDropdowns.DropdownAction.options[_actionsDropdowns.DropdownAction.value].text;
        string[] parameters = _actionCreatorBehaviour.Actions[SelectedIndex].ActionParameters;
        _button.GetComponentInChildren<Text>().text = FormatAction.ActionToString(actionClass, actionName, parameters);
    }
    
    public void DestroyActionVisual()
    {
        Destroy(_button.gameObject);
    }
}