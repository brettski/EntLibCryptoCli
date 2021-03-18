using EntLibCryptoCli.Options;
using System;
using System.IO;
using System.Text;

namespace EntLibCryptoCli.Service
{
    public static class GenerateKeyService
    {

        public static int RunGenerate(GenerateKey GenerateKeyOpts)
        {
            // Checks
            int inError = 0;
            StringBuilder errMsgText = new StringBuilder();

            try
            {
                using (FileStream tfs = File.OpenWrite(GenerateKeyOpts.KeyFile))
                { }
            }
            catch (UnauthorizedAccessException)
            {
                inError = 2;
                errMsgText.Append($"- Unable to open output file, '{GenerateKeyOpts.KeyFile}', for writting\n");
            }
            catch (DirectoryNotFoundException)
            {
                inError = 3;
                errMsgText.Append($"- Directory not found, cannot create output file: '{GenerateKeyOpts.KeyFile}'\n");
            }
            catch (Exception ex)
            {
                inError = 9;
                errMsgText.Append($"- Unknown error encountered:\n{ex.Message}\n");
            }

            if (GenerateKeyOpts.Password.Trim() == string.Empty)
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
            FileInfo generateFile = new FileInfo(GenerateKeyOpts.KeyFile);
            bool result = GenerateSecureKey.Generate(generateFile, GenerateKeyOpts.Password);
            if (!result)
            {
                Console.Write($"\nUnable to generate secure key.");
                return 5;
            }
            if (File.Exists(GenerateKeyOpts.KeyFile) && new FileInfo(GenerateKeyOpts.KeyFile).Length > 10)
            {
                Console.Write($"\nKey, {GenerateKeyOpts.KeyFile}, created successfully!\n");
                return 0;
            }
            else
            {
                Console.Write($"\nKey, {GenerateKeyOpts.KeyFile}, creation failed!\n");
                return -1;
            }
        }
    }
}
