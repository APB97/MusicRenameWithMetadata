using System;
using System.Reflection;

namespace FileMetadata.Dynamic
{
    public static class Shell
    {
        public static dynamic NameSpace(dynamic shell, string folderPath)
        {
            Type type = shell.GetType();
            return type.InvokeMember(nameof(NameSpace), BindingFlags.InvokeMethod, null, shell,
                new object[] {folderPath});
        }

        public static dynamic Items(dynamic folder)
        {
            Type type = folder.GetType();
            return type.InvokeMember(nameof(Items), BindingFlags.InvokeMethod, null, folder, new object[] { });
        }

        public static dynamic ExtendedProperty(dynamic item, string property)
        {
            Type type = item.GetType();
            return type.InvokeMember(nameof(ExtendedProperty), BindingFlags.InvokeMethod, null, item, new object[]
                {
                    property
                });
        }

        public static string Path(dynamic item)
        {
            Type type = item.GetType();
            return type.InvokeMember(nameof(Path), BindingFlags.GetProperty, null, item, new object[] { });
        }

        public static dynamic GetFolderItems(string folderPath)
        {
            var shellAppType = Type.GetTypeFromProgID("Shell.Application");
            if (shellAppType == null) return null;
            dynamic shellApp = Activator.CreateInstance(shellAppType);
            if (shellApp == null) return null;
            dynamic folder = NameSpace(shellApp, folderPath);
            if (folder == null) return null;
            var folderItems = Items(folder);
            return folderItems;
        }
    }
}