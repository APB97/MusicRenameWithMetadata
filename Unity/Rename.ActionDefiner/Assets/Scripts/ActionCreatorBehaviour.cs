using System.Collections.Generic;
using JsonStructures;
using UnityEngine;
using UnityEngine.UI;

public class ActionCreatorBehaviour : MonoBehaviour
{
    [SerializeField] private Text actionClass;
    [SerializeField] private Text actionName;
    [SerializeField] private InputField path;

    private ActionsCreator _creator;
    private ParametersCreatorBehaviour _parametersCreator;
    private IActionVisualizer _visualizer;
    private ISelectedAction _selectionInfo;

    public IActionsArray Actions => _creator;

    public ParametersCreatorBehaviour ParametersCreator => _parametersCreator;

    private void Awake()
    {
        _creator = new ActionsCreator(new List<ActionDefinition>());
        _parametersCreator = GetComponent<ParametersCreatorBehaviour>();
        _visualizer = GetComponent<IActionVisualizer>();
        _selectionInfo = GetComponent<ISelectedAction>();
    }

    public void Load(string filePath)
    {
        foreach (ActionDefinition definition in _creator.LoadFile(filePath))
        {
            _visualizer.AddVisualFor(definition);
        }
    }
    
    public void Add()
    {
        var parameters = _parametersCreator.PopAllParameters();
        ActionDefinition definition = _creator.Add(actionClass.text, actionName.text, parameters.ToArray());
        _visualizer.AddVisualFor(definition);
    }

    public void CreateFile()
    {
        _creator.CreateFile(path.text);
    }

    public void UpdateCurrentElement()
    {
        var parameters = _parametersCreator.PopAllParameters();
        _creator.UpdateAt(_selectionInfo.SelectedIndex, actionClass.text, actionName.text, parameters.ToArray());
    }

    public void RemoveCurrentElement()
    {
        _creator.RemoveAt(_selectionInfo.SelectedIndex);
        _parametersCreator.ClearParams();
    }
}