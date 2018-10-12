using EntLibCryptoCli.Model;
using Microsoft.Practices.EnterpriseLibrary.Security.Cryptography;
using System;
using System.IO;
using System.Security.Cryptography;

namespace EntLibCryptoCli
{
    public static class ArchiveKey
    {
        public static ArchiveKeyResult Archive(string ArchiveFile, 
                                string Password, 
                                FileInfo SecuredKey)
        {
            ArchiveKeyResult result = new ArchiveKeyResult
            {
                IsInError = false,
                ArchiveKey = ArchiveFile,
                SecuredKey = SecuredKey.FullName,
                Password = Password
            };

            ProtectedKey _securedKey;
            try
            {
                using (FileStream rfs = File.OpenRead(SecuredKey.FullName))
                {
                    _securedKey = KeyManager.Read(rfs, DataProtectionScope.LocalMachine);
                }

                using (FileStream ofs = File.OpenWrite(ArchiveFile))
                {
                    KeyManager.ArchiveKey(ofs, _securedKey, Password);
                }
            } catch (Exception ex)
            {
                result.IsInError = true;
                result.ErrorString = ex.Message;
                result.Exception = ex;
            }

            return result;
        }
    }
}
