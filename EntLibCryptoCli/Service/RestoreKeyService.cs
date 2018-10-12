using System;
using System.IO;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Security.Cryptography;
using System.Security.Cryptography;
using EntLibCryptoCli.Model;

namespace EntLibCryptoCli.Service
{
    public static class RestoreKeyService
    {

        public static int RunRestore(Options.RestoreKey RestoreKeyOpts)
        {
            // Checks
            int inError = 0;
            StringBuilder errMsgText = new StringBuilder();

            if (!File.Exists(RestoreKeyOpts.ImportFile))
            {
                // Import file Doesn't exist 
                inError = 1;
                errMsgText.Append($"- Provided import file, '{RestoreKeyOpts.ImportFile}', doesn't exist.\n");
            }

            try
            {
                using (FileStream tfs = File.OpenWrite(RestoreKeyOpts.OutputKeyFile))
                { }
            } catch (UnauthorizedAccessException)
            {
                inError = 2;
                errMsgText.Append($"- Unable to open output file, '{RestoreKeyOpts.OutputKeyFile}', for writting\n");
            } catch (DirectoryNotFoundException)
            {
                inError = 3;
                errMsgText.Append($"- Directory not found, cannot create output file: '{RestoreKeyOpts.OutputKeyFile}'\n");
            } catch (Exception ex)
            {
                inError = 9;
                errMsgText.Append($"- Unknown error encountered:\n{ex.Message}\n");
            }

            if (RestoreKeyOpts.Password.Trim() == string.Empty)
            {
                inError = 4;
                errMsgText.Append($"- Provided password value is empty or contains only whitespace\n");
            }

            if (inError > 0)
            {
                Console.Write($"\nThere is a problem with the provided parameters:\n\n{errMsgText.ToString()}\n\n");
                return inError;
            }

            // Work

            FileInfo importFile = new FileInfo(RestoreKeyOpts.ImportFile);
            RestoreSecureKeyResult result = RestoreSecureKey.Restore(importFile, RestoreKeyOpts.Password, RestoreKeyOpts.OutputKeyFile);
            if (result.IsInError)
            {
                Console.Write($"\nUnable to restore secure key. Exception message is:\n{result.ErrorString}\n");
                return 5;
            }
            if (File.Exists(RestoreKeyOpts.OutputKeyFile) && new FileInfo(RestoreKeyOpts.OutputKeyFile).Length > 10)
            {
                Console.Write($"\nKey, {RestoreKeyOpts.OutputKeyFile}, created successfully!\n");
                return 0;
            }
            else
            {
                Console.Write($"\nKey, {RestoreKeyOpts.OutputKeyFile}, creation failed!\n");
                return -1;
            }
        }
    }
}
