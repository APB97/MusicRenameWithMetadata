using Console;
using System;
using System.Collections.Generic;

namespace MusicMetadataRenamer.Wpf
{
    internal class DummyConsole : ISilenceAbleConsole
    {
        public bool Silent => true;

        public IEnumerable<string> CommandsForJson => Array.Empty<string>();

        public void WriteLine(string text)
        {
            
        }

        public void Clear()
        {
            
        }

        public void WriteLine(object value)
        {
            
        }

        public void BeSilent()
        {

        }

        public void DontBeSilent()
        {

        }

        public string GetHelpFor(string command)
        {
            return string.Empty;
        }
    }
}