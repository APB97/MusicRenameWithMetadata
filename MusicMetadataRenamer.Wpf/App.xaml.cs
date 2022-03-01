using Console;
using FileMetadata.Dynamic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using MusicMetadataRenamer.Wpf.ViewModel;
using Rename.Helpers;
using StringProcessor;
using StringProcessor.SkipCommonWords;
using System.Windows;

namespace MusicMetadataRenamer.Wpf
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var collection = new ServiceCollection();
            collection.AddSingleton<ConsoleViewModel>();
            collection.AddSingleton<ISilenceAbleConsole, DummyConsole>();
            collection.AddSingleton<IConsole>(ioc => ioc.GetRequiredService<ConsoleViewModel>());
            collection.AddSingleton<DirectorySelector>();
            collection.AddSingleton<PropertySelector>();
            collection.AddSingleton<SkipFile>();
            collection.AddSingleton<MetadataRename>();
            collection.AddSingleton<RenameFiles>();
            collection.AddSingleton<IStringProcessor>(ioc => new SkipCommonWordsProcessor 
            {
                CommonWords = WordSkipping.GetCommonWordsFrom(ioc.GetRequiredService<SkipFile>().SelectedPath)
            });

            Ioc.Default.ConfigureServices(new DefaultServiceProviderFactory().CreateServiceProvider(collection));
        }
    }
}