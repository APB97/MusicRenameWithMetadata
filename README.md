# Rename Music with Metadata
Rename music files using their metadata with this tool.

## Contents

1. [What does it do?](#what-does-it-do)
2. [Who is it for?](#who-is-it-for)
3. [Why is it different?](#why-is-it-different)
    1. [Interactive mode](#interactive-mode)
    2. [Batch mode](#batch-mode)
4. [How do I use it?](#how-do-i-use-it)
    1. [Console](#console)
    2. [Directory Selector](#directory-selector)
5. [How does it work?](#how-does-it-work)

## What does it do?
It allows users to specify properties to use for renaming and directories with files they want to rename.
Apart from that, all invalid path characters are removed to ensure renaming operation will be successful.
There is default file _skip.txt_ for words user want to remove from final filenames.
Renaming is done automatically after specifying all required settings. 

## Who is it for?
This project is for users wanting to rename many files at once with their files' metadata.

## Why is it different?
This tool features two modes used for renaming files: __Interactive__ and __Batch__.

### Interactive mode
First one is the Interactive mode, started when program is launched without arguments.
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
For list of possible definitions and commands, see [How do I use it?](#how-do-i-use-it)

## How do I use it?

### Console

- Enable __Silent__ mode - no console output

Enter `BeSilent` Command in Interactive Mode during Console step

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

- Disable __Silent__ mode - re-enables console output

Enter `DontBeSilent` Command in Interactive Mode

### Directory Selector

- Add directories to processing list

Enter `Add <directory-without-spaces> [<another-one>] [...]`, for example:

`Add C:\Music D:\Music`

or with spaces:

`Add "C:\Music\Example - 1" "C:\Music\Example - 2"`

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

- Clear directories-to-process list

Enter `Clear`

- Complete directory selection step

Enter `Complete`

- Clear console screen

Enter `ClearScreen`

- Display help for all or selected commands

Enter `Help` to display help for all commands

Enter `Help command1 [command2] [...]` to display help for given commands, for example:

Enter `Help Add Remove Clear` to display help for Add, Remove and Clear commands

## How does it work?

It works by reading properties from *.mp3 files using their ID3v2 metadata containers, currently only `Artists` (TPE1) and `Title` (TIT2) are supported.
These values are used for renaming music files in given directories, however they are additionally processed to skip invalid path characters and words included in a skip file.
Default skip file is supplied in `skipfile.txt`.