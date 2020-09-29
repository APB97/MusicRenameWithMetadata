using System.Collections.Generic;
using System.Linq;

namespace FileMetadata.Dynamic
{
    public class Metadata
    {
        /// <summary>
        /// Get Dictionary mapping folderItems to Dictionary of properties and their values.
        /// </summary>
        /// <param name="folderItems">Result of <see cref="Shell"/>.<see cref="Shell.GetFolderItems"/></param>
        /// <param name="propertyFullNames">Collection of full property names.</param>
        /// <returns>Dictionary representing as pairs (folderItem, propertyValues)
        /// where propertyValues is Dictionary of pairs (property, value).</returns>
        public static Dictionary<dynamic, Dictionary<string, string>> GetProperties(dynamic folderItems,
            IEnumerable<string> propertyFullNames)
        {
            var filesPropertiesDictionary = new Dictionary<dynamic, Dictionary<string, string>>();

            // Prevent multiple Enumeration
            IEnumerable<string> fullNamesArray = propertyFullNames as string[] ?? propertyFullNames.ToArray();
            // Iterate over folders' Items
            foreach (dynamic folderItem in folderItems)
            {
                // Create a dictionary for file's properties
                Dictionary<string, string> singleFileProperties = new Dictionary<string, string>();
                // Iterate over property full names
                foreach (var fullName in fullNamesArray)
                {
                    // Get Property Value
                    dynamic propertyValue = Shell.ExtendedProperty(folderItem, fullName);
                    // Check underlying type
                    switch (propertyValue)
                    {
                        // When property is of type: string
                        case string _:
                            // Simply add its value
                            singleFileProperties.Add(fullName, propertyValue);
                            break;
                        // When property is an array
                        case string[] array:
                            // Add it as joined comma-separated string
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