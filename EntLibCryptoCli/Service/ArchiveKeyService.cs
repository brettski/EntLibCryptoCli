using EntLibCryptoCli.Model;
using System;
using System.IO;
using System.Text;

namespace EntLibCryptoCli.Service
{
    public static class ArchiveKeyService
    {
        public static int RunExport(Options.ExportKey ExportKeyOpts)
        {
            // Checks
            int inError = 0;
            StringBuilder errMsgText = new StringBuilder();

            if (!File.Exists(ExportKeyOpts.KeyFile))
            {
                // Provided key file doesn't exist
                inError = 10;
                errMsgText.Append($"- Provided import file, {ExportKeyOpts.KeyFile}, doesn't exist.\n");
            }

            try
            {
                using (FileStream tef = File.OpenWrite(ExportKeyOpts.Exportfile))
                { }
            } catch (UnauthorizedAccessException)
            {
                inError = 11;
                errMsgText.Append($"- Unable to open output file, {ExportKeyOpts.Exportfile}, for writting\n");
            } catch (DirectoryNotFoundException)
            {
                inError = 12;
                errMsgText.Append($"- Directory not found: {ExportKeyOpts.Exportfile}\n");
            } catch (Exception ex)
            {
                inError = 19;
                errMsgText.Append($"- Other error occured:\n{ex.Message}\n");
            }

            if (ExportKeyOpts.Password.Trim() == string.Empty)
            {
                inError = 13;
                errMsgText.Append($"- Provided password value is empty ({ExportKeyOpts.Password})\n");
            }

            if (inError > 0 )
            {
                Console.Write($"\nTHere is a problem with the provided parameters:\n\n{errMsgText.ToString()}");
                return inError;
            }

            // Do stuff

            FileInfo keyToArchive = new FileInfo(ExportKeyOpts.KeyFile);
            ArchiveKeyResult result = ArchiveKey.Archive(ExportKeyOpts.Exportfile, ExportKeyOpts.Password, keyToArchive);
            if (result.IsInError)
            {
                Console.Write($"\nUnable to archive secure key. Exception message is:\n{result.ErrorString}");
                return 15;
            }

            if (File.Exists(ExportKeyOpts.Exportfile) && new FileInfo(ExportKeyOpts.Exportfile).Length > 10)
            {
                Console.Write($"\nKey archive created successfully. To transfer the key, copy to another computer and restore it.");
                return 0;
            }
            else
            {
                Console.Write($"\nKey archive creation failed.");
                return -1;
            }
        }
    }
}
