﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FileMetadata.Dynamic;

namespace MusicMetadataRenamer
{
    public class PropertySelector : SelectorBase
    {
        protected override HashSet<string> Commands { get; } = new HashSet<string>(
        new []
        {
            nameof(Add),
            nameof(Clear),
            nameof(Complete),
            nameof(ClearScreen),
            nameof(Help),
            nameof(HelpCommands),
            nameof(HelpProperties),
            nameof(List)
        });

        public List<string> Properties { get; } = new List<string>();

        public virtual IEnumerable<string> StartInteractive()
        {
            while (true)
            {
                Console.WriteLine($"{nameof(PropertySelector)} - Type \'Help\' for help:");

                string line = Console.ReadLine();
                string[] inputs = line?.Split(' ');
                string command = inputs?[0];

                if (string.IsNullOrWhiteSpace(command) || !Commands.Contains(command))
                    continue;

                MethodInfo methodInfo = GetType().GetMethod(command);
                object callResult = methodInfo?.Invoke(this,
                    methodInfo.GetParameters().Length == 1
                        ? new object[] {inputs.Skip(1).ToArray()}
                        : new object[] { });

                if (callResult is bool shouldComplete && shouldComplete)
                    return Properties;
            }
        }

        public virtual void Add(params string[] properties)
        {
            foreach (string property in properties)
            {
                if (Properties.Contains(property))
                {
                    Console.WriteLine($"{property} is already on the list so it wasn't added.");
                }
                else
                {
                    Properties.Add(property);
                }
            }

            Console.WriteLine("Properties added to list.");
        }

        public override void Clear()
        {
            Properties.Clear();
            Console.WriteLine("Property list cleared.");
        }

        public override void Help()
        {
            base.Help();
            HelpProperties();
        }

        public virtual void HelpCommands()
        {
            base.Help();
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
    }
}