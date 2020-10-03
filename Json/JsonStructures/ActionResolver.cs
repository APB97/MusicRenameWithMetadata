﻿using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JsonStructures
{
    public class ActionResolver
    {
        private readonly Dictionary<string, object> _classDefaultObjects;

        /// <summary>
        /// Create new instance of resolver
        /// </summary>
        /// <param name="keyObjectPairs">Pairs (key, object) to use when resolving Actions</param>
        public ActionResolver(KeyValuePair<string, object>[] keyObjectPairs)
        {
            _classDefaultObjects = new Dictionary<string, object>(keyObjectPairs);
        }

        /// <summary>
        /// Executes Action(s) from given file.
        /// </summary>
        /// <param name="actionsFile">path to JSON file with commands.</param>
        /// <returns>awaitable Task</returns>
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