using Console;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;

namespace MusicMetadataRenamer.Wpf.ViewModel
{
    public class ConsoleViewModel : ObservableObject, ISilenceAbleConsole
    {
        private bool silent;
        private string text;

        public bool Silent
        {
            get => silent;
            set
            {
                silent = value;
                OnPropertyChanged(nameof(Silent));
            }
        }

        public IEnumerable<string> CommandsForJson => Array.Empty<string>();

        public string Text
        {
            get => text;
            set
            {
                if (text != value)
                {
                    text = value;
                    OnPropertyChanged(nameof(Text));
                }
            }
        }

        public void BeSilent()
        {
            silent = true;
        }

        public void Clear()
        {
            Text = string.Empty;
        }

        public void DontBeSilent()
        {
            silent = false;
        }

        public string GetHelpFor(string command)
        {
            return string.Empty;
        }

        public void WriteLine(string text)
        {
            if (Silent) return;
            Text += $"{text}{Environment.NewLine}";
        }

        public void WriteLine(object value)
        {
            if (Silent) return;
            WriteLine(value.ToString());
        }
    }
}
