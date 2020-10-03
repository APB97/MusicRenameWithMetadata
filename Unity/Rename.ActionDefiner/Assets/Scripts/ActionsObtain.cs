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

    private readonly Dictionary<string, object> _commandObjects = new Dictionary<string, object>();
    
    private void Awake()
    {
        SilenceAbleConsole console = new SilenceAbleConsole();
        _commandObjects[console.ToString()] = console;
        
        DirectorySelector dirSelector  = new DirectorySelector(console);
        _commandObjects[dirSelector.ToString()] = dirSelector;
        
        List<string> options = new List<string>();
        options.AddRange(_commandObjects.Keys);
     
        dropdownActionClasses.ClearOptions();
        
        dropdownActionClasses.AddOptions(options);
        dropdownActionClasses.onValueChanged.AddListener(OnSelectedIndex);
        OnSelectedIndex(default);
    }

    public void OnSelectedIndex(int index)
    {
        dropdownActions.ClearOptions();
        
        if (_commandObjects[dropdownActionClasses.options[index].text] is ICommandClass commandObject)
            dropdownActions.AddOptions(commandObject.CommandsForJson.ToList());
    }
}