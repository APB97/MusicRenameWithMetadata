# Rename Music with Metadata
Rename music files using their metadata with this tool.

## What does it do?
It allows users to specify properties to use for renaming and directories with files they want to rename.
Apart from that, all invalid path characters are removed to ensure renaming operation will be successful.
There is default file _skip.txt_ for words user want to remove from final filenames.
Renaming is done automatically after specifying all required settings. 

## Who is it for?
This project is for __Windows__ users wanting to rename many files at once with their files' metadata.

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
For list of possible definitions see [How do I use it?](#how-do-i-use-it)

## How do I use it?

### Console

- Enable __Silent__ mode - no console output

```json
{
  "Actions": [
    {
      "ActionClass": "Console",
      "ActionName": "BeSilent",
      "ActionParameters": []
    }
  ]
}
```

### Directory Selector

- Add directories to processing list
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

## How does it work?

