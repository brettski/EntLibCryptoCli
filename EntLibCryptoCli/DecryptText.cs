using Microsoft.Practices.EnterpriseLibrary.Security.Cryptography;

namespace EntLibCryptoCli
{
    public static class DecryptText
    {
        public static string Decrypt(string encryptedtext)
        {
            return Cryptographer.DecryptSymmetric("ForTestingProvider", encryptedtext);
        }
    }
}
