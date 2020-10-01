using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Rename.Helpers;

namespace MusicMetadataRenamer
{
    public class ActionResolver
    {
        private readonly Dictionary<string, object> _classDefaultObjects;

        public ActionResolver(KeyValuePair<string, object>[] keyObjectPairs)
        {
            _classDefaultObjects = new Dictionary<string, object>(keyObjectPairs);
        }

        public async Task Execute(string actionsFile)
        {
            if (!File.Exists(actionsFile))
                return;

            var definitions = await GetActionsFromFile(actionsFile);
            if (definitions.Actions == null)
                return;
            
            InvokeActions(definitions);
        }

        private static async Task<ActionDefinitions> GetActionsFromFile(string actionsFile)
        {
            string actionsFileContents = await File.ReadAllTextAsync(actionsFile);
            return JsonConvert.DeserializeObject<ActionDefinitions>(actionsFileContents);
        }

        private void InvokeActions(ActionDefinitions definitions)
        {
            foreach (ActionDefinition action in definitions.Actions)
            {
                object defaultObject = _classDefaultObjects[action.ActionClass];
                MethodInfo method = defaultObject.GetType().GetMethod(action.ActionName);
                method?.Invoke(defaultObject,
                    method.GetParameters().Length == 0 ? new object[0] : new object[] {action.ActionParameters});
            }
        }
    }
}