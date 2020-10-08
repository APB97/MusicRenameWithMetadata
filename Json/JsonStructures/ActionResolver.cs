using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using CommandClassInterface;
using Newtonsoft.Json;

namespace JsonStructures
{
    /// <summary>
    /// Allows execution of commands from a JSON file
    /// </summary>
    public class ActionResolver
    {
        private readonly Dictionary<string, ICommandClass> _classDefaultObjects;

        /// <summary>
        /// Create new instance of resolver
        /// </summary>
        /// <param name="keyObjectPairs">Pairs (key, object) to use when resolving Actions</param>
        public ActionResolver(KeyValuePair<string, ICommandClass>[] keyObjectPairs)
        {
            _classDefaultObjects = new Dictionary<string, ICommandClass>();
            foreach (KeyValuePair<string, ICommandClass> pair in keyObjectPairs)
            {
                _classDefaultObjects.Add(pair.Key, pair.Value);
            }
        }

        /// <summary>
        /// Executes Action(s) from given file.
        /// </summary>
        /// <param name="actionsFile">path to JSON file with commands.</param>
        public async Task Execute(string actionsFile)
        {
            if (!File.Exists(actionsFile))
                return;

            var definitions = GetActionsFromFile(actionsFile);
            if (definitions.Actions == null)
                return;
            
            await InvokeActions(definitions);
        }

        private static ActionDefinitions GetActionsFromFile(string actionsFile)
        {
            string actionsFileContents = File.ReadAllText(actionsFile);
            return JsonConvert.DeserializeObject<ActionDefinitions>(actionsFileContents);
        }

        private async Task InvokeActions(ActionDefinitions definitions)
        {
            foreach (ActionDefinition action in definitions.Actions)
            {
                ICommandClass defaultObject = _classDefaultObjects[action.ActionClass];
                MethodInfo method = defaultObject.GetType().GetMethod(action.ActionName);
                await Task.Run(() => method?.Invoke(defaultObject,
                    method.GetParameters().Length == 0 ? new object[0] : new object[] {action.ActionParameters}));
            }
        }
    }
}