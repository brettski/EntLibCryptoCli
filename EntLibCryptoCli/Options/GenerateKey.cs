using CommandLine;

namespace EntLibCryptoCli.Options
{
    [Verb("generatekey", HelpText = "Generates a RijndaelManaged key using System.Security.Cryptography and saves it to a password protected file that can be restored to different machines.")]
    public class GenerateKey
    {
        [Option('k', "keyfile", Required = true, HelpText = "File location and name where key will be exported.")]
        public string KeyFile { get; set; }

        [Option('p', "password", Required = true, HelpText = "Password used to secure exported key.")]
        public string Password { get; set; }
    }
}
