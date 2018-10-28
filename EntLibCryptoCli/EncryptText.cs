using Microsoft.Practices.EnterpriseLibrary.Security.Cryptography;

namespace EntLibCryptoCli
{
    public static class EncryptText
    {
        public static string Encrypt(string plaintext)
        {
            return Cryptographer.EncryptSymmetric("ForTestingProvider", plaintext);
        }
    }
}
