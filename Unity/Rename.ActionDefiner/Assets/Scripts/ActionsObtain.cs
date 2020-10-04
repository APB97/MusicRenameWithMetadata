using System.Collections.Generic;
using System.Linq;
using CommandClassInterface;
using Console;
using Rename.Helpers;
using UnityEngine;
using UnityEngine.UI;

public class ActionsObtain : MonoBehaviour
{
    [SerializeField] private Dropdown dropdownActionClasses;
    [SerializeField] private Dropdown dropdownActions;

    [SerializeField] private Text helpDisplay;
    
    private readonly Dictionary<string, object> _commandObjects = new Dictionary<string, object>();
    private ICommandClass _currentClass;

    private void Awake()
    {
        SilenceAbleConsole console = new SilenceAbleConsole();
        _commandObjects[console.ToString()] = console;
        
        PropertySelector propertySelector = new PropertySelector(console);
        _commandObjects[propertySelector.ToString()] = propertySelector;
        
        DirectorySelector dirSelector  = new DirectorySelector(console);
        _commandObjects[dirSelector.ToString()] = dirSelector;
        
        SkipFile skipFile = new SkipFile(console);
        _commandObjects[skipFile.ToString()] = skipFile;
        
        List<string> options = new List<string>();
        options.AddRange(_commandObjects.Keys);
     
        dropdownActionClasses.ClearOptions();
        
        dropdownActionClasses.AddOptions(options);
        dropdownActionClasses.onValueChanged.AddListener(OnSelectedClass);
        dropdownActions.onValueChanged.AddListener(OnSelectedAction);
        OnSelectedClass(default);
    }

    private void OnSelectedClass(int index)
    {
        dropdownActions.ClearOptions();

        if (!(_commandObjects[dropdownActionClasses.options[index].text] is ICommandClass commandObject)) return;
        _currentClass = commandObject;
        dropdownActions.AddOptions(commandObject.CommandsForJson.ToList());
        OnSelectedAction(default);
    }

    private void OnSelectedAction(int index)
    {
        helpDisplay.text = _currentClass.GetHelpFor(dropdownActions.options[index].text);
    }
}