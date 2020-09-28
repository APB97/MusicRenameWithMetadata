using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FileMetadata.Dynamic;
// ReSharper disable UnusedMember.Global

namespace MusicMetadataRenamer
{
    public class PropertySelector
    {
        private readonly List<string> _properties = new List<string>();

        private static readonly HashSet<string> Commands = new HashSet<string>(
        new []{
            nameof(Add),
            nameof(ClearScreen),
            nameof(Help),
            nameof(HelpCommands),
            nameof(HelpProperties),
            nameof(List)
        });

        public List<string> Properties => _properties;

        public virtual IEnumerable<string> StartInteractive()
        {
            while (true)
            {
                Console.WriteLine($"{nameof(PropertySelector)} - Type \'Help\' for help:");

                string line = Console.ReadLine();
                string[] inputs = line?.Split(' ');
                string command = inputs?[0];

                if (string.IsNullOrWhiteSpace(line))
                    return Properties;

                if (!Commands.Contains(command))
                    continue;

                MethodInfo methodInfo = GetType().GetMethod(command);
                methodInfo?.Invoke(this,
                    methodInfo.GetParameters().Length == 1
                        ? new object[] {inputs.Skip(1).ToArray()}
                        : new object[] { });
            }
        }

        public virtual void Add(params string[] properties)
        {
            Properties.AddRange(properties);

            Console.WriteLine("Properties added.");
        }

        public virtual void Help()
        {
            HelpCommands();
            HelpProperties();
        }

        public virtual void HelpCommands()
        {
            Console.WriteLine("Available Commands:");
            foreach (string methodName in Commands)
            {
                Console.WriteLine(methodName);
            }
        }

        public virtual void HelpProperties()
        {
            Console.WriteLine("Available Properties:");
            foreach (FieldInfo field in typeof(PropertyNames).GetFields())
            {
                Console.WriteLine(field.GetValue(null));
            }
        }

        public virtual void List()
        {
            Console.WriteLine("Selected Properties:");
            foreach (string property in Properties)
            {
                Console.WriteLine(property);
            }
        }

        public virtual void ClearScreen()
        {
            Console.Clear();
        }
    }
}