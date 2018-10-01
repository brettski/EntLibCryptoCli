using System;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Security.Cryptography;
using System.Security.Cryptography;
using EntLibCryptoCli.Model;

namespace EntLibCryptoCli
{
    public static class RestoreSecureKey
    {
        /// <summary>
        /// Restores a Cryptographic key from previously exported file. Use to transfer key to new computer
        /// </summary>
        /// <param name="ImportFile">Previously exported symmetric key to be restored</param>
        /// <param name="Password">Password used to secure previously exported key</param>
        /// <param name="NewProviderKey">Location and filename of key to be restored.</param>
        /// <returns></returns>
        public static RestoreSecureKeyResult Restore(FileInfo ImportFile,
                                  string Password,
                                  string NewProviderKey)
        {
            RestoreSecureKeyResult result = new RestoreSecureKeyResult
            {
                IsInError = false,
                ImportFile = ImportFile.FullName,
                RestoredKey = NewProviderKey,
                Password = Password
            };
            ProtectedKey RestoredSecureKey;
            KeyManager.ClearCache();

            try
            {
                using (FileStream fs = File.OpenRead(ImportFile.FullName))
                {
                    RestoredSecureKey = KeyManager.RestoreKey(fs, Password, DataProtectionScope.CurrentUser);
                }

                using (FileStream ofs = File.OpenWrite(NewProviderKey))
                {
                    KeyManager.Write(ofs, RestoredSecureKey);
                }
            } catch (Exception ex)
            {
                result.IsInError = true;
                result.ErrorString = ex.Message.ToString();
                if (result.ErrorString.StartsWith("Padding is invalid and cannot be removed"))
                    result.ErrorString += "\nIt may be due to an incorrect password.";
                result.Exception = ex;
            }

            return result;
        }

    }
}
