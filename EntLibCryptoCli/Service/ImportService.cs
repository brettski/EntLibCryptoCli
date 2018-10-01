using System;
using System.IO;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Security.Cryptography;

namespace EntLibCryptoCli.Service
{
    public static class ImportService
    {

        public static int RunImport(Options.Import ImportOpts)
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
                Console.Write($"/nThere is a problem with the provided options:\n\n{errMsgText.ToString()}");
                return inError;
            }

            // Work
            Console.WriteLine("This is work time now.");

            /*
            string exportKeyFile = @"C:\Dlls\Valtera.Namespace 2.0\EngagementProviderExport.txt";
            string exportKeyPw = "@password";
            string saveKeyFile = @"C:\Dlls\Valtera.Namespace 2.0\bin\EngagementProvider.key";
            ProtectedKey NewServerKey;

            using (FileStream fs = File.OpenRead(exportKeyFile))
            {
	            NewServerKey = KeyManager.RestoreKey(fs, exportKeyPw, System.Security.Cryptography.DataProtectionScope.LocalMachine);
	            NewServerKey.Dump();
            }

            NewServerKey.Dump();

            using (FileStream ofs = File.OpenWrite(saveKeyFile)) 
            {
	            KeyManager.Write(ofs, NewServerKey);

            }
            */

            return 0;
        }
    }
}
