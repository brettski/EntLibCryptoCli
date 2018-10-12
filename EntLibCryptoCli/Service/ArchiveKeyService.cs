using EntLibCryptoCli.Model;
using EntLibCryptoCli.Utilities;
using System;
using System.IO;
using System.Text;

namespace EntLibCryptoCli.Service
{
    public static class ArchiveKeyService
    {
        public static int RunArchive(Options.ArchiveKey ArchiveKeyOpts)
        {
            // Checks
            int inError = 0;
            StringBuilder errMsgText = new StringBuilder();

            if (!File.Exists(ArchiveKeyOpts.KeyFile))
            {
                // Provided key file doesn't exist
                inError = 10;
                errMsgText.Append($"- Provided import file, {ArchiveKeyOpts.KeyFile}, doesn't exist.\n");
            }

            try
            {
                using (FileStream tef = File.OpenWrite(ArchiveKeyOpts.Archivefile))
                { }
            } catch (UnauthorizedAccessException)
            {
                inError = 11;
                errMsgText.Append($"- Unable to open output file, {ArchiveKeyOpts.Archivefile}, for writting\n");
            } catch (DirectoryNotFoundException)
            {
                inError = 12;
                errMsgText.Append($"- Directory not found: {ArchiveKeyOpts.Archivefile}\n");
            } catch (Exception ex)
            {
                inError = 19;
                errMsgText.Append($"- Other error occured:\n{ex.Message}\n");
            }

            if (ArchiveKeyOpts.Password.Trim() == string.Empty)
            {
                inError = 13;
                errMsgText.Append($"- Provided password value is empty ({ArchiveKeyOpts.Password})\n");
            }

            ValidatePasswordResult pwresult = Validate.Password(ArchiveKeyOpts.Password.Trim());
            if (pwresult.IsFailed)
            {
                inError = 14;
                errMsgText.Append($"- Failed password validation:\n\n{pwresult.FailedReason.Replace("-", "  -")}\n");
            }

            if (inError > 0 )
            {
                Console.Write($"\nTHere is a problem with the provided parameters:\n\n{errMsgText.ToString()}");
                return inError;
            }

            // Do stuff

            FileInfo keyToArchive = new FileInfo(ArchiveKeyOpts.KeyFile);
            ArchiveKeyResult result = ArchiveKey.Archive(ArchiveKeyOpts.Archivefile, ArchiveKeyOpts.Password, keyToArchive);
            if (result.IsInError)
            {
                Console.Write($"\nUnable to archive secure key. Exception message is:\n{result.ErrorString}");
                return 15;
            }

            if (File.Exists(ArchiveKeyOpts.Archivefile) && new FileInfo(ArchiveKeyOpts.Archivefile).Length > 10)
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
