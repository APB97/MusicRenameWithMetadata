using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using FileMetadata.Dynamic;
using Newtonsoft.Json;
using Rename.Helpers;
using Rename.Helpers.Interfaces;

namespace MusicMetadataRenamer
{
    public class RenameActionResolver
    {
        private readonly Dictionary<string, object> _classDefaultObjects;
        private readonly IPropertyList _propertySelector;
        private readonly IDirectorySet _directorySelector;
        private readonly ConsoleWrapper _console;

        public RenameActionResolver()
        {
            _console = new ConsoleWrapper();
            _propertySelector = new PropertySelector(_console);
            _directorySelector = new DirectorySelector(_console);
            
            _classDefaultObjects = new Dictionary<string, object>(new []
            {
                new KeyValuePair<string, object>(nameof(PropertySelector), _propertySelector),
                new KeyValuePair<string, object>(nameof(DirectorySelector), _directorySelector),
                new KeyValuePair<string, object>(nameof(ConsoleWrapper), _console)
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

            var wordsToSkip = new WordSkipping();
            await wordsToSkip.GetCommonWordsFrom("skip.txt");
            new Rename(_console).Execute(_directorySelector, _propertySelector, wordsToSkip, new MetadataRename(_console));
        }
    }
}