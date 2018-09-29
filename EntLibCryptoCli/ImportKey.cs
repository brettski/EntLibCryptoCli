using System;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Security.Cryptography;
using System.Security.Cryptography;

namespace EntLibCryptoCli
{
    public static class ImportKey
    {
        /// <summary>
        /// Restores a Cryptographic key from previously exported file. Use to transfer key to new computer
        /// </summary>
        /// <param name="ImportFile">Previously exported symmetric key to be restored</param>
        /// <param name="Password">Password used to secure previously exported key</param>
        /// <param name="NewProviderKey">Location and filename of key to be restored.</param>
        /// <returns></returns>
        public static bool Import(FileInfo ImportFile,
                                  string Password,
                                  string NewProviderKey)
        {
            ProtectedKey RestoredSecureKey;

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
                Console.Write("That didn't work\n");
                Console.Write(ex.Message);

            }

            return false;
        }

    }
}
