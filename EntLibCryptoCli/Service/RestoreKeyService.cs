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
                inError++;
                errMsgText.Append($"- Provided import file, {RestoreKeyOpts.ImportFile}, doesn't exist.\n");
            }

            try
            {
                using (FileStream tfs = File.OpenWrite(RestoreKeyOpts.OutputKeyFile))
                { }
            } catch (UnauthorizedAccessException)
            {
                inError++;
                errMsgText.Append($"- Unable to open output file, {RestoreKeyOpts.OutputKeyFile}, for writting\n");
            } catch (DirectoryNotFoundException)
            {
                inError++;
                errMsgText.Append($"- Directory not found: {RestoreKeyOpts.OutputKeyFile}");
            } catch (Exception ex)
            {
                inError++;
                errMsgText.Append($"- Other error encountered:\n{ex.InnerException.ToString()}");
            }

            if (RestoreKeyOpts.Password.Trim() == string.Empty)
            {
                inError++;
                errMsgText.Append($"- Provided password value, {RestoreKeyOpts.Password.Trim()}, is empty\n");
            }

            if (inError>0)
            {
                Console.Write($"\nThere is a problem with the provided parameters:\n\n{errMsgText.ToString()}");
                return inError;
            }

            // Work

            FileInfo importFile = new FileInfo(RestoreKeyOpts.ImportFile);
            RestoreSecureKeyResult result = RestoreSecureKey.Restore(importFile, RestoreKeyOpts.Password, RestoreKeyOpts.OutputKeyFile);
            if (result.IsInError)
            {
                Console.Write($"\nUnable to restore secure key. Exception message is:\n{result.ErrorString}\n");
                return 10;
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
            //return 0;
        }
    }
}
