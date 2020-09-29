using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MusicMetadataRenamer
{
    public class RenameActionResolver
    {
        private readonly Dictionary<string, object> _classDefaultObjects;
        private readonly IPropertyList _propertySelector;
        private readonly IDirectorySet _directorySelector;

        public RenameActionResolver()
        {
            var console = new ConsoleWrapper();
            _propertySelector = new PropertySelector(console);
            _directorySelector = new DirectorySelector(console);
            
            _classDefaultObjects = new Dictionary<string, object>(new []
            {
                new KeyValuePair<string, object>(nameof(PropertySelector), _propertySelector),
                new KeyValuePair<string, object>(nameof(DirectorySelector), _directorySelector),
                new KeyValuePair<string, object>(nameof(ConsoleWrapper), console)
            });
        }
        
        public async Task Execute(string actionsFile)
        {
            var definitions = JsonConvert.DeserializeObject<ActionDefinitions>(await File.ReadAllTextAsync(actionsFile));

            foreach (ActionDefinition action in definitions.Actions)
            {
                object defaultObject = _classDefaultObjects[action.ActionClass];
                MethodInfo method = defaultObject.GetType().GetMethod(action.ActionName);
                method?.Invoke(defaultObject, method.GetParameters().Length == 0 ? new object[0] : new object[]{ action.ActionParameters });
            }
            
            new Rename(_classDefaultObjects[nameof(ConsoleWrapper)] as ConsoleWrapper).Execute(_directorySelector, _propertySelector);
        }
    }
}