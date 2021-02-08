using Microsoft.Practices.EnterpriseLibrary.Security.Cryptography;
using System.IO;
using System.Security.Cryptography;

namespace EntLibCryptoCli.Service
{
    public static class GenerateSecureKey
    {
        /// <summary>
        /// Restores a Cryptographic key from previously exported file. Use to transfer key to new computer
        /// </summary>
        /// <param name="ImportFile">Previously exported symmetric key to be restored</param>
        /// <param name="Password">Password used to secure previously exported key</param>
        /// <param name="NewProviderKey">Location and filename of key to be restored.</param>
        /// <returns></returns>
        public static bool Generate(string Password, FileInfo NewKeyFile)
        {
            KeyManager.ClearCache();

            byte[] managerByteKey = KeyManager.GenerateSymmetricKey("Rijndael");
            ProtectedKey newProtectedKey = ProtectedKey.CreateFromPlaintextKey(managerByteKey, DataProtectionScope.LocalMachine);

            using (FileStream ofs = File.OpenWrite(NewKeyFile.FullName))
            {
                KeyManager.ArchiveKey(ofs, newProtectedKey, Password);
            }

            return true;
        }
    }
}