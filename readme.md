# EntLibCryptoCli

## Enterprise Library Cryptography Block CLI

Born out of a requirement to restore RijndaelManaged Symmetric keys from the command line and removing the manual use of the EntLibConfig.exe app.

Inputs: A previously Archived (exported) key, the password for the exported key

Ouputs: A new machine level DPAPI protected RijndaelManaged provider key.

## Basic use

Get general options and available commands:  
`EntLibCryptoCli -h`

Get options for a specific command:  
`EntLibCryptoCli <command>`

Archive a working DPAPI protected key so you may transfer it to another computer:  
`EntLibCryptoCli archivekey -k c:\pathto\keyfile.key -p Str@ongPw -a c:\path\archivefile.txt`

Restore an archived key on a computer:  
`EntLibCryptoCli restorekey -a c:\path\archivefile.txt -k c:\path\restore\newkeyfile.key -p Str@ongPw`
