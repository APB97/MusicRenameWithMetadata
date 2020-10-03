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
}