using Microsoft.Practices.EnterpriseLibrary.Security.Cryptography;
using System.IO;
using System.Security.Cryptography;

namespace EntLibCryptoCli.Service
{
    public static class GenerateSecureKey
    {
        /// <summary>
        /// Generates a new Cryptographic key from Security.Cryptography. Use to initiate new basis for encrypting objects.
        /// </summary>
        /// <param name="NewKeyFile">Name of new keyfile to be generated</param>
        /// <param name="Password">Password used to secure the new keyfile</param>
        /// <returns></returns>
        public static bool Generate(FileInfo NewKeyFile, string Password )
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