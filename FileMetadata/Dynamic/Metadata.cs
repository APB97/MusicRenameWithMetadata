using System.Collections.Generic;
using System.Linq;

namespace FileMetadata.Dynamic
{
    public class Metadata
    {
        public static Dictionary<dynamic, Dictionary<string, string>> GetProperties(dynamic folderItems,
            IEnumerable<string> propertyFullNames)
        {
            var filesPropertiesDictionary = new Dictionary<dynamic, Dictionary<string, string>>();

            IEnumerable<string> fullNamesArray = propertyFullNames as string[] ?? propertyFullNames.ToArray();
            foreach (dynamic folderItem in folderItems)
            {
                Dictionary<string, string> singleFileProperties = new Dictionary<string, string>();
                foreach (var fullName in fullNamesArray)
                {
                    dynamic propertyValue = Shell.ExtendedProperty(folderItem, fullName);
                    switch (propertyValue)
                    {
                        case string _:
                            singleFileProperties.Add(fullName, propertyValue);
                            break;
                        case string[] array:
                            singleFileProperties.Add(fullName, string.Join(", ", array));
                            break;
                    }
                }

                if (singleFileProperties.Count > 0)
                    filesPropertiesDictionary[folderItem] = singleFileProperties;
            }
            
            return filesPropertiesDictionary;
        }
    }
}