# Rename Music with Metadata
> Rename music files using their metadata with this tool.

## Table of Contents

1. [Description](#description)
    1. [Interactive mode](#interactive-mode)
    2. [Batch mode](#batch-mode)
2. [How to use](#how-to-use)
    1. [Installation](#installation)
    2. [WPF App](#wpf-app)
    3. [Interactive mode](#interactive-mode)
    4. [Batch mode](#batch-mode)
3. [API Reference](#api-reference)
    1. [Console](#console)
    2. [Directory Selector](#directory-selector)

## Description
It allows users to specify properties to use for renaming and directories with files they want to rename.
Apart from that, all invalid path characters are removed to ensure renaming operation will be successful.
There is default file `skip.txt` for words user want to remove from final filenames.
Renaming is done automatically after specifying all required settings.

Currently only Album, Artists and Title properties are available.
This tool features two modes used for renaming files: __Interactive__ and __Batch__.

### Technologies
- C#
- .NET
    - Extension methods
    - Reflection
    - Resource file(s)
- NuGet Packages
    - Newtonsoft.Json
    - Microsoft.Toolkit.Mvvm
    - Microsoft.Extensions.DependencyInjection
- Markdown

[Back to the top](#rename-music-with-metadata)

## How to use

### Installation

Before using this project, you need to compile it, for example using Visual Studio or Rider.

Executable filename that can be used to launch will be `MusicMetadataRenamer.exe` for console version and `MusicMetadataRenamer.Wpf.exe` for WPF application.

### WPF App

You can use the WPF version, which has Directory, Property and SkipFile selection.
It shows output from changes to settings but currently does not show any output from renaming files.

It also allows users to save current settings to `.json` file to load them later.

You can see the user inteface here:

<!--
![WPF UI](https://user-images.githubusercontent.com/16359542/156168928-344dd277-749f-44f1-b56e-031d571e3ec9.png)
-->
![WPF UI](https://user-images.githubusercontent.com/16359542/156198027-c851deed-c757-4741-8eff-8d3adfed205d.png)


### Interactive mode
First available mode of the console version is the Interactive mode, started when program is launched without arguments.
It allows to check/use available options for each step involved.
All steps feature a `Help` command used to check available commands and their descriptions and usage.

Currently it starts at `PropertySelector` step used to select which properties to include in new filenames and their order in filenames.
To complete this step, simply type `Complete` command.

Then app proceeds to `DirectorySelector` step. It allows user to specify which directories to include during processing of filenames.
To complete this step, simply type `Complete` command.

After that comes the last, skip-able step - to skip it press Enter.
But if you want to specify different file with words to skip than skip.txt, then you should enter its path and then press Enter.

That's all! Application will rename files at given path(s).

### Batch mode
Second mode is triggered when application is launched with single argument - path to a `JSON` file containing action definitions.
Example of such a file is included in `Actions.json`.

For list of possible definitions and commands, see [API Reference](#api-reference)

[Back to the top](#rename-music-with-metadata)

## API Reference

### Console

- To enable __Silent__ mode - no console output
    - Enter `BeSilent` Command in Interactive Mode during any of first two steps:

    - Or use the following JSON object:
    ```json
    {
      "Actions": [
        {
          "ActionClass": "Console",
          "ActionName": "BeSilent"
        }
      ]
    }
    ```

- To disable __Silent__ mode - re-enables console output

    - Enter `DontBeSilent` Command in Interactive Mode
    
    - Or use the following JSON object:
    ```json
    {
      "Actions": [
        {
          "ActionClass": "Console",
          "ActionName": "DontBeSilent"
        }
      ]
    }
    ```
### Directory Selector

- To add directories to processing list:

    - Enter `Add <directory-without-spaces> [<another-one>] [...]`, for example:
      
      `Add C:\Music D:\Music`

       or with spaces:

       `Add "C:\Music\Example - 1" "C:\Music\Example - 2"`

    - Or Use the following JSON object:
    
    ```json
    {
      "Actions": [
        {
          "ActionClass": "DirectorySelector",
          "ActionName": "Add",
          "ActionParameters": [
            "C:\\Music\\Example - 1",
            "C:\\Music\\Example - 2"
          ]
        }
      ]
    }
    ```

- To clear directories-to-process list:

    - Enter `Clear`

- To complete directory selection step:

    - Enter `Complete`

- To clear console screen

    - Enter `ClearScreen`

- To display help for all or selected commands

    - Enter `Help` to display help for all commands
    
    - Enter `Help command1 [command2] [...]` to display help for given commands, for example:
    
        - Enter `Help Add Remove Clear` to display help for Add, Remove and Clear commands

- To display directories-to-process list
    
    - Enter `List`
    
- To Remove directories from directories-to-process list

    - Enter `Remove directory1 [directory2] [...]`
    
        - Enter `Remove C:\Music\Pending` to remove `C:\Music\Pending` directory from the list

[Back to the top](#rename-music-with-metadata)
