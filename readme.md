# EntLibCryptoCli

## Enterprise Library Cryptography Block CLI

Born out of a requirement to create new RijndaelManaged Symmetric keys from the command line and removing the manual use of the EntLibConfig.exe app.

Inputs: A previously exported key, a password for the exported key

Ouputs: A new machine level DPAPI protected RijndaelManaged provider key.