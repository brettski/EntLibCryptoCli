# EntLibCryptoCli

[![Build status](https://brettski.visualstudio.com/EntLibCryptoCli/_apis/build/status/EntLibCryptoCli-.NET%20Desktop-CI)](https://brettski.visualstudio.com/EntLibCryptoCli/_build/latest?definitionId=1)
[![release](https://img.shields.io/badge/Release-v1.2.0-blue.svg)](https://github.com/brettski/EntLibCryptoCli/releases/tag/v1.3.0)

## Enterprise Library Cryptography Block CLI

Born out of a requirement to restore RijndaelManaged Symmetric keys from the command line and removing the manual use of the EntLibConfig.exe app. Why does that matter? The EntLibConfig.exe application is a desktop app with no command line support.

So if you have an older application which depends on the Enterprise Library Cryptography Block and which to run that application in an auto-scaling group, you will not be able to as the RijndaelManaged key is DPAPI protected and must be `restored` on each new computer it is used on. Before this CLI, that could only be done through a manual clickity-clicky app.  Well unless you roll your own of course :).

Inputs: A previously Archived (exported) key, the password for the exported key

Ouputs: A new machine level DPAPI protected RijndaelManaged provider key.

## Basic Use

### Built-in help

Get general options and available commands:  
`EntLibCryptoCli --help`

Get options for a specific command:  
`EntLibCryptoCli <command> --help`

> Note: help will always be shown if a command is not used correctly along with wrong or missing parameters.

### Commands

Get version information:  
`EntLibCryptoCli --version`

Archive a working DPAPI protected key so you may transfer it to another computer:  
`EntLibCryptoCli archivekey -k c:\pathto\keyfile.key -p Str@ongPw -a c:\path\archivefile.txt`

Restore an archived key on a computer:  
`EntLibCryptoCli restorekey -a c:\path\archivefile.txt -k c:\path\restore\newkeyfile.key -p Str@ongPw`

## Basic Functionality Test

In the release package is a folder named TestKey. There is an exported (archived) key (ExportedKey.txt) located here which may be restored for testing encryption and decryption with the provided encrypted text.

Note: DO NOT use this key for your own projects! It is provided only as a convenience to allow you to to test the cli's functionality on your machine.

### Process

After downloading the release from [GitHub](https://github.com/brettski/EntLibCryptoCli/releases), unzip the file to `c:\`. For the sake of these instructions we will assume the release is in folder `c:\EntLibCryptoCli`. Since the location of the key is important to use it for encrypting and decrypting ensure it's exported to the TestKey folder as `UseForVerificationOnly.key`.

1. From the command line ensure you're in the `c:\EntLibCryptoCli` folder
1. Restore key to your computer.
    1. > `EntLibCryptoCli restorekey -a c:\EntLibCryptoCli\TestKey\ExportedKey.txt -k c:\EntLibCryptoCli\TestKey\UseForVerificationOnly.key -p p@ssw0rD`
1. Decrypt text in file `TBD`
    1. > `EntLibCryptoCli decrypt -f ~filename~`
    1. Output value: `TBD`
