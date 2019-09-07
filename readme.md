# EntLibCryptoCli

[![Build status](https://brettski.visualstudio.com/EntLibCryptoCli/_apis/build/status/EntLibCryptoCli-.NET%20Desktop-CI)](https://brettski.visualstudio.com/EntLibCryptoCli/_build/latest?definitionId=1)
[![release](https://img.shields.io/badge/Release-v1.3.0-blue.svg)](https://github.com/brettski/EntLibCryptoCli/releases/tag/v1.3.0)

- [Enterprise Library Cryptography Block CLI](#Enterprise-Library-Cryptography-Block-CLI)
- [Basic Use](#Basic-Use)
  - [Built-in help](#Built-in-help)
  - [Commands](#Commands)
- [Basic Functionality Test](#Basic-Functionality-Test)
  - [Process](#Process)
- [Troubleshooting](#Troubleshooting)

## Enterprise Library Cryptography Block CLI

Born out of a requirement to restore RijndaelManaged Symmetric keys from the command line while removing the **manual** use of the `EntLibConfig.exe` app. Why does that matter? The `EntLibConfig.exe` application is a desktop application with no command line support.

If you have an older application which depends on the [Enterprise Library Cryptography Block](https://www.nuget.org/packages/EnterpriseLibrary.Security.Cryptography/) and wish to run that application in an auto-scaling group, you will not be able to as the RijndaelManaged key is DPAPI protected and must be *restored* on each new computer it is used on. Before this CLI, that could only be done through a manual clickity-clickity-click-click app.  Well unless you rolled your own of course :).

- Input: A previously Archived (exported) key, the password for the exported key

- Output: A new machine level DPAPI protected RijndaelManaged provider key.

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

Note: **DO NOT** use this key for your own projects! It is provided only as a convenience to allow you to to test the cli's functionality on your machine.

### Process

After downloading the release from [GitHub](https://github.com/brettski/EntLibCryptoCli/releases), unzip the file to `c:\`. For the sake of these instructions we will assume the release is in folder `c:\EntLibCryptoCli`. Since the location of the key is important to use it for encrypting and decrypting ensure it's exported to the TestKey folder as `UseForVerificationOnly.key`.

1. From the command line ensure you're in  the `c:\EntLibCryptoCli` folder
1. Restore key to your computer.
    1. > `EntLibCryptoCli restorekey -a c:\EntLibCryptoCli\TestKey\ExportedKey.txt -k c:\EntLibCryptoCli\TestKey\UseForVerificationOnly.key -p p@ssw0rD`
1. Decrypt the test encrypted text: `H8tpiA7eRcAT+V3qb9TD2nEYwoShvpPkfcwOtf8HP/28Tbh9Utznkf9VpG8qaAzzY2k+kxrnaZ821t3BsYwPNoRlw5x9Uf92BbWwioTnO2sA+guYR7vEXYtVrES/LEZ1ULrdOi90K/hFUiKmESNe2/A2SsYyZ+ocgh2pKUXLplY=`
    1. > `EntLibCryptoCli decrypt -s H8tpiA7eRcAT+V3qb9TD2nEYwoShvpPkfcwOtf8HP/28Tbh9Utznkf9VpG8qaAzzY2k+kxrnaZ821t3BsYwPNoRlw5x9Uf92BbWwioTnO2sA+guYR7vEXYtVrES/LEZ1ULrdOi90K/hFUiKmESNe2/A2SsYyZ+ocgh2pKUXLplY=`
    1. Output value will be legible if correct.
    1. A Message like, "The data is invalid", probably means the key is corrupt
    1. A message like, "Padding is invalid and cannot be removed", usually means the key has not been restored correctly on the computer it is being used on.
1. If you like you may encrypt some text:
    1. `EntLibCryptoCli encrypt -s "some text"`
1. The decrypt it again:
    1. `EntLibCryptoCli decrypt -s LKFqWSVE3OsScUvgnuP7/KBJCqgmX2qtmJhfvWXz4ZebTYSHbINwrncX8Qmt29xt`
        - Please note that your encrypted string WILL be different due to the way AES encryption work. Though this string will decrypt to the same value.

## Troubleshooting

The biggest issue I have come across with the cli is testing the encryption and decryption. Since the library must be registered in app.config file there may be a version conflict depending on the current version pulled from NuGet.

To date no issues have been seen restoring provider keys.
