using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MusicMetadataRenamer
{
    public class RenameActionResolver
    {
        private readonly Dictionary<string, object> _classDefaultObjects;
        private readonly PropertySelector _propertySelector;
        private readonly DirectorySelector _directorySelector;

        public RenameActionResolver()
        {
            _propertySelector = new PropertySelector();
            _directorySelector = new DirectorySelector();
            
            _classDefaultObjects = new Dictionary<string, object>(new []
            {
                new KeyValuePair<string, object>(nameof(PropertySelector), _propertySelector),
                new KeyValuePair<string, object>(nameof(DirectorySelector), _directorySelector), 
            });
        }
        
        public async Task Execute(string actionsFile)
        {
            var definitions = JsonConvert.DeserializeObject<ActionDefinitions>(await File.ReadAllTextAsync(actionsFile));

            foreach (ActionDefinition action in definitions.Actions)
            {
                object defaultObject = _classDefaultObjects[action.ActionClass];
                defaultObject.GetType().GetMethod(action.ActionName)
                    ?.Invoke(defaultObject, new object[]{ action.ActionParameters });
            }
            
            new Rename().Execute(_directorySelector, _propertySelector);
        }
    }
}