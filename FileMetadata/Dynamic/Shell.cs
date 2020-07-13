using System;
using System.Reflection;

namespace FileMetadata.Dynamic
{
    public static class Shell
    {
        /// <summary>
        /// Returns instance of NameSpace (folder) for later use.
        /// </summary>
        /// <param name="shell">Shell instance.</param>
        /// <param name="folderPath">Path to directory.</param>
        /// <returns>Instance of NameSpace.</returns>
        public static dynamic NameSpace(dynamic shell, string folderPath)
        {
            Type type = shell.GetType();
            return type.InvokeMember(nameof(NameSpace), BindingFlags.InvokeMethod, null, shell,
                new object[] {folderPath});
        }

        /// <summary>
        /// Returns collection of FolderItems in a NameSpace (Folder).
        /// </summary>
        /// <param name="folder">Folder instance. Call <see cref="NameSpace"/> to obtain the instance.</param>
        /// <returns>Collection of FolderItems</returns>
        public static dynamic Items(dynamic folder)
        {
            Type type = folder.GetType();
            return type.InvokeMember(nameof(Items), BindingFlags.InvokeMethod, null, folder, new object[] { });
        }

        /// <summary>
        /// Get extended property of a folder item by its full name.
        /// </summary>
        /// <param name="item">A folder item.</param>
        /// <param name="property">Full property name.</param>
        /// <returns>Value of the property.</returns>
        public static dynamic ExtendedProperty(dynamic item, string property)
        {
            Type type = item.GetType();
            return type.InvokeMember(nameof(ExtendedProperty), BindingFlags.InvokeMethod, null, item, new object[]
                {
                    property
                });
        }
        
        /// <summary>
        /// Get full path to the given FolderItem. 
        /// </summary>
        /// <param name="folderItem">FolderItem to get its path.</param>
        /// <returns>Path to FolderItem.</returns>
        public static string Path(dynamic folderItem)
        {
            Type type = folderItem.GetType();
            return type.InvokeMember(nameof(Path), BindingFlags.GetProperty, null, folderItem, new object[] { });
        }
        
        /// <summary>
        /// Get FolderItems of given folder Path.
        /// </summary>
        /// <param name="folderPath">Path to folder to get FolderItems from.</param>
        /// <returns>Enumerable collection of FolderItems.</returns>
        public static dynamic GetFolderItems(string folderPath)
        {
            var shellApp = CreateShellAppInstance();
            if (shellApp == null) return null;
            dynamic folder = NameSpace(shellApp, folderPath);
            if (folder == null) return null;
            var folderItems = Items(folder);
            return folderItems;
        }
        
        private static dynamic CreateShellAppInstance()
        {
            var shellAppType = Type.GetTypeFromProgID("Shell.Application");
            if (shellAppType == null) return null;
            dynamic shellApp = Activator.CreateInstance(shellAppType);
            return shellApp;
        }
    }
}