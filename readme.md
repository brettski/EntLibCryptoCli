# EntLibCryptoCli

[![Build status](https://brettski.visualstudio.com/EntLibCryptoCli/_apis/build/status/EntLibCryptoCli-.NET%20Desktop-CI)](https://brettski.visualstudio.com/EntLibCryptoCli/_build/latest?definitionId=1)

## Enterprise Library Cryptography Block CLI

Born out of a requirement to restore RijndaelManaged Symmetric keys from the command line and removing the manual use of the EntLibConfig.exe app. Why does that matter? The EntLibConfig.exe application is a desktop app with no command line support. 

So if you have an older application which depends on the Enterprise Library Crypography Block and which to run that appliation in an autoscalling group, you will not be able to as the RijndaelManaged key is DPAPI protected and must be `restored` on each new computer it is used on. Before this CLI, that could only be done through a manual clickity-clicky app.  Well unless you roll your own of course :).

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
