using System.Collections.Generic;
using JsonStructures;
using UnityEngine;
using UnityEngine.UI;

public class ActionCreatorBehaviour : MonoBehaviour
{
    [SerializeField] private Text actionClass;
    [SerializeField] private Text actionName;
    [SerializeField] private InputField path;

    [SerializeField] private Button buttonUpdate;
    [SerializeField] private Button buttonRemove;
    
    private ActionsCreator _creator;
    private int? _selectedIndex;
    private Button _button;
    private ParametersCreatorBehaviour _parametersCreator;
    private ActionVisualizer _visualizer;
    
    public ActionVisualizer Visualizer => _visualizer;

    private void Awake()
    {
        _creator = new ActionsCreator(new List<ActionDefinition>());
        _parametersCreator = GetComponent<ParametersCreatorBehaviour>();
        _visualizer = GetComponent<ActionVisualizer>();
    }

    public void Load(string filePath)
    {
        foreach (ActionDefinition definition in _creator.LoadFile(filePath))
        {
            Visualizer.AddVisualFor(definition);
        }
    }
    
    public void Add()
    {
        var parameters = _parametersCreator.PopAllParameters();
        ActionDefinition definition = _creator.Add(actionClass.text, actionName.text, parameters.ToArray());
        Visualizer.AddVisualFor(definition);
    }

    public void SelectForUpdate(int indexOfAdded, Button button)
    {
        _selectedIndex = indexOfAdded;
        _button = button;

        var definition = _creator.ElementAt(indexOfAdded);
        actionClass.text = definition.ActionClass;
        Dropdown dropdownClass = actionClass.GetComponentInParent<Dropdown>();
        dropdownClass.value = dropdownClass.options.FindIndex(data => data.text == definition.ActionClass);
        
        actionName.text = definition.ActionName;
        Dropdown dropdownName = actionName.GetComponentInParent<Dropdown>();
        dropdownName.value = dropdownName.options.FindIndex(data => data.text == definition.ActionName);
        
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
        var parameters = _parametersCreator.PopAllParameters();
        _creator.UpdateAt(_selectedIndex, actionClass.text, actionName.text, parameters.ToArray());
        _button.GetComponentInChildren<Text>().text = FormatAction.ActionToString(actionClass.text, actionName.text, parameters);
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