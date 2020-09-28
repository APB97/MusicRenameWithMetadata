using System.Collections.Generic;
using System.Linq;

namespace FileMetadata.Dynamic
{
    public class Metadata
    {
        public static Dictionary<dynamic, Dictionary<string, string>> GetProperties(dynamic folderItems,
            IEnumerable<string> propertyFullNames)
        {
            var dict = new Dictionary<dynamic, Dictionary<string, string>>();

            IEnumerable<string> fullNames = propertyFullNames as string[] ?? propertyFullNames.ToArray();
            foreach (var folderItem in folderItems)
            {
                Dictionary<string, string> innerDict = new Dictionary<string, string>();
                foreach (var fullName in fullNames)
                {
                    var propertyValue = Shell.ExtendedProperty(folderItem, fullName);
                    switch (propertyValue)
                    {
                        case string _:
                            innerDict.Add(fullName, propertyValue);
                            break;
                        case string[] strArray:
                            innerDict.Add(fullName, string.Join(", ", strArray));
                            break;
                    }
                }

                if (innerDict.Count > 0)
                    dict[folderItem] = innerDict;
            }
            
            return dict;
        }
    }
}