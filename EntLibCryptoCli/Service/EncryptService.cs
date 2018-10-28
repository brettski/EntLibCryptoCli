using System;
using System.IO;
using System.Text;

namespace EntLibCryptoCli.Service
{
    public static class EncryptService
    {
        public static int RunEncrypt(Options.Encrypt EncryptOptions)
        {
            int inError = 0;
            StringBuilder errMsgText = new StringBuilder();

            if (EncryptOptions.PlainTextString == null)
                EncryptOptions.PlainTextString = string.Empty;
            if (EncryptOptions.PlanTextFile == null)
                EncryptOptions.PlanTextFile = string.Empty;

            if (EncryptOptions.PlainTextString?.Length > 0 && EncryptOptions.PlanTextFile.Length > 0)
            {
                // Cannot use plaintext string and file option at the same time.
                inError++;
                errMsgText.Append($"- Using both --textstring and --textfile options together is not supported.");
            }

            if (EncryptOptions.PlainTextString.Length == 0 && EncryptOptions.PlanTextFile.Length == 0)
            {
                inError++;
                errMsgText.Append($"- Either --textstring OR --textfile option must be provided. You are not providing anything to decrypt!");
            }

            if (EncryptOptions.PlainTextString.Length == 0 && EncryptOptions.PlanTextFile.Length > 0)
            {
                if (!File.Exists(EncryptOptions.PlanTextFile))
                {
                    inError++;
                    errMsgText.Append($"- Provided plain text file, {EncryptOptions.PlanTextFile}, doesn't exist.");
                }
            }

            if (inError > 0)
            {
                Console.Write($"\nThere is a problem with provided parameters (--help for help):\n\n{errMsgText.ToString()}\n");
                return inError;
            }

            // work time
            string valueToEncrypt;
            if (EncryptOptions.PlanTextFile.Length > 0)
            {
                try
                {
                    valueToEncrypt = File.ReadAllText(EncryptOptions.PlanTextFile);
                } catch (UnauthorizedAccessException)
                {
                    Console.Write($"- Error reading plain text file, unauthorized.\n");
                    return 101;
                } catch (DirectoryNotFoundException)
                {
                    Console.Write($"- Eror reading plain text file, Directory not found!\n");
                    return 102;
                } catch (Exception ex)
                {
                    Console.Write($"- An unknown error has occurred:\n\n{ex.Message}\n\n");
                    return 110;
                }
            } else if (EncryptOptions.PlainTextString.Length > 0)
            {
                valueToEncrypt = EncryptOptions.PlainTextString;
            } else
            {
                Console.Write($"- PlainTextString and PlanTextFile are empty. We shouldn't be here, must be missing a check!");
                return 111;
            }

            string result;
            try
            {
                result = EncryptText.Encrypt(valueToEncrypt);
            } catch (Exception ex)
            {
                Console.Write($"- Error while encrypting string:\n\n{ex.Message}\n\n");
                return 112;
            }
            Console.Write(result);
            return 0;
        }
    }
}
