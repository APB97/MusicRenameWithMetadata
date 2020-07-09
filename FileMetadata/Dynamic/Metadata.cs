using System.Collections.Generic;

namespace FileMetadata.Dynamic
{
    public class Metadata
    {
        public static Dictionary<dynamic, Dictionary<string, string>> GetProperties(dynamic folderItems,
            string[] propertyFullNames)
        {
            var dict = new Dictionary<dynamic, Dictionary<string, string>>();

            foreach (var folderItem in folderItems)
            {
                int namesArrayLength = propertyFullNames.Length;
                Dictionary<string, string> innerDict = new Dictionary<string, string>();
                for (int i = 0; i < namesArrayLength; i++)
                {
                    var propertyValue = Shell.ExtendedProperty(folderItem, propertyFullNames[i]);
                    switch (propertyValue)
                    {
                        case string _:
                            innerDict.Add(propertyFullNames[i], propertyValue);
                            break;
                        case string[] strArray:
                            innerDict.Add(propertyFullNames[i], string.Join(", ", strArray));
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