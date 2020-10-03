using System.Collections.Generic;
using System.IO;
using JsonStructures;
using Newtonsoft.Json;

public class ActionsCreator
{
    private readonly List<ActionDefinition> _definitionsToSerialize;
    
    public ActionsCreator(List<ActionDefinition> definitionsToSerialize)
    {
        _definitionsToSerialize = definitionsToSerialize;
    }

    public void Add(string defaultCommandObject, string actionName, params string[] parameters)
    {
        ActionDefinition definition = new ActionDefinition()
        {
            ActionClass = defaultCommandObject,
            ActionName = actionName,
        };
        
        if (parameters != null && parameters.Length > 0)
            definition.ActionParameters = parameters;
        
        _definitionsToSerialize.Add(definition);
    }

    public void CreateFile(string pathText)
    {
        string json = JsonConvert.SerializeObject(new ActionDefinitions() {Actions = _definitionsToSerialize.ToArray()}, Formatting.Indented);
        File.WriteAllText(pathText, json);
    }

    public void UpdateAt(int? selectedIndex, string actionClassText, string actionNameText, params string[] parameters)
    {
        if (selectedIndex.HasValue) _definitionsToSerialize[selectedIndex.Value] = new ActionDefinition
        {
            ActionClass = actionClassText,
            ActionName = actionNameText,
            ActionParameters = parameters
        };
    }

    public void RemoveAt(int? selectedIndex)
    {
        if (selectedIndex.HasValue) _definitionsToSerialize.RemoveAt(selectedIndex.Value);
    }

    public ActionDefinition ElementAt(int index)
    {
        return _definitionsToSerialize[index];
    }
}