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

        public static int RunRestore(Options.RestoreKey ImportOpts)
        {
            // Checks
            int inError = 0;
            StringBuilder errMsgText = new StringBuilder();

            if (!File.Exists(ImportOpts.ImportFile))
            {
                // Import file Doesn't exist 
                inError++;
                errMsgText.Append($"- Provided import file, {ImportOpts.ImportFile}, doesn't exist.\n");
            }

            try
            {
                using (FileStream tfs = File.OpenWrite(ImportOpts.OutputKeyFile))
                { }
            } catch (UnauthorizedAccessException)
            {
                inError++;
                errMsgText.Append($"- Unable to open output file, {ImportOpts.OutputKeyFile}, for writting\n");
            } catch (DirectoryNotFoundException)
            {
                inError++;
                errMsgText.Append($"- Directory not found: {ImportOpts.OutputKeyFile}");
            } catch (Exception ex)
            {
                inError++;
                errMsgText.Append($"- Other error encountered:\n{ex.InnerException.ToString()}");
            }

            if (ImportOpts.Password.Trim() == string.Empty)
            {
                inError++;
                errMsgText.Append($"- Provided password value, {ImportOpts.Password.Trim()}, is empty\n");
            }

            if (inError>0)
            {
                Console.Write($"\nThere is a problem with the provided parameters:\n\n{errMsgText.ToString()}");
                return inError;
            }

            // Work

            FileInfo importFile = new FileInfo(ImportOpts.ImportFile);
            RestoreSecureKeyResult result = RestoreSecureKey.Restore(importFile, ImportOpts.Password, ImportOpts.OutputKeyFile);
            if (result.IsInError)
            {
                Console.Write($"Unable to restore secure key. Exception message is:\n{result.Exception.Message}");
                return 10;
            }
            if (File.Exists(ImportOpts.OutputKeyFile) && new FileInfo(ImportOpts.OutputKeyFile).Length > 10)
            {
                Console.Write($"Key, {ImportOpts.OutputKeyFile}, created successfully!");
                return 0;
            }
            else
            {
                Console.Write($"Key, {ImportOpts.OutputKeyFile}, creation failed!");
                return -1;
            }
            //return 0;
        }
    }
}
