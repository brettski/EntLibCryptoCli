using System;
using System.IO;
using System.Text;

namespace EntLibCryptoCli.Service
{
    public static class DecryptService
    {
        public static int RunDecrypt(Options.Decrypt DecryptOptions)
        {
            int inError = 0;
            StringBuilder errMsgText = new StringBuilder();

            if (DecryptOptions.EncryptedString == null)
                DecryptOptions.EncryptedString = string.Empty;
            if (DecryptOptions.EncryptedTextFile == null)
                DecryptOptions.EncryptedTextFile = string.Empty;

            if (DecryptOptions.EncryptedString.Length > 0 && DecryptOptions.EncryptedTextFile.Length > 0)
            {
                // Cannot use string and file at the same time.
                inError++;
                errMsgText.Append($"- Using both --encryptedstring and --encryptedfile options together is not supported");
            }

            if (DecryptOptions.EncryptedString.Length == 0 && DecryptOptions.EncryptedTextFile.Length == 0)
            {
                inError++;
                errMsgText.Append($"- Either --encryptedstring or --encryptedfile option must be provided.");
            }

            if (DecryptOptions.EncryptedString.Length == 0 && DecryptOptions.EncryptedTextFile.Length > 0)
            {
                if (!File.Exists(DecryptOptions.EncryptedTextFile))
                {
                    inError++;
                    errMsgText.Append($"- Provided encrypted text file, {DecryptOptions.EncryptedTextFile}, doesn't exist.");
                }
            }

            if (inError > 0)
            {
                Console.Write($"\nThere is a problem with provided parameters (--help for help):\n\n{errMsgText.ToString()}\n");
            }

            string valueToDecrypt;
            if (DecryptOptions.EncryptedTextFile.Length > 0)
            {
                try
                {
                    valueToDecrypt = File.ReadAllText(DecryptOptions.EncryptedTextFile);
                }
                catch (UnauthorizedAccessException)
                {
                    Console.Write($"- Error reading encrypted text file, unauthorized.\n");
                    return 201;
                }
                catch (DirectoryNotFoundException)
                {
                    Console.Write($"- Eror reading encrypted text file, Directory not found!\n");
                    return 202;
                }
                catch (Exception ex)
                {
                    Console.Write($"- An unknown error has occurred:\n\n{ex.Message}\n\n");
                    return 210;
                }
            } else if (DecryptOptions.EncryptedString.Length > 0)
            {
                valueToDecrypt = DecryptOptions.EncryptedString;
            } else
            {
                Console.Write($"- EncryptedString and EncryptedTextFile are empty. We shouldn't be here, must be missing a check!");
                return 211;
            }

            string result;
            try
            {
                result = DecryptText.Decrypt(valueToDecrypt);
            } catch (Exception ex)
            {
                Console.Write($"- Error while decrypting string:\n\n{ex.Message}\n\n");
                return 212;
            }
            Console.Write(result);
            return 0;
        }
    }
}
