using System.Collections.Generic;
using System.IO;
using System.Linq;
using JsonStructures;
using Newtonsoft.Json;

namespace Actions
{
    public class ActionsCreator : IActionsArray
    {
        private List<ActionDefinition> _definitionsToSerialize;
    
        public ActionsCreator(List<ActionDefinition> definitionsToSerialize)
        {
            _definitionsToSerialize = definitionsToSerialize;
        }

        public ActionDefinition Add(string defaultCommandObject, string actionName, params string[] parameters)
        {
            ActionDefinition definition = new ActionDefinition()
            {
                ActionClass = defaultCommandObject,
                ActionName = actionName,
            };
        
            if (parameters != null && parameters.Length > 0)
                definition.ActionParameters = parameters;
        
            _definitionsToSerialize.Add(definition);
            return definition;
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

        public ActionDefinition this[int index] => _definitionsToSerialize[index];

        public IEnumerable<ActionDefinition> LoadFile(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return _definitionsToSerialize = JsonConvert.DeserializeObject<ActionDefinitions>(json).Actions.ToList();
        }
    }
}