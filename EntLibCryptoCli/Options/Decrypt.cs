using CommandLine;

namespace EntLibCryptoCli.Options
{
    [Verb("decrypt", HelpText = "Decrypt text from a command line or file")]
    public class Decrypt
    {
        [Option('s', "encryptedstring", SetName = "text", Required = true, HelpText = "A string of encrypted text to decrypt. Cannot be used with --encryptedfile option.")]
        public string EncryptedString { get; set; }

        [Option('f', "encryptedfile", SetName = "file", Required = true, HelpText = "A file of encrypted text to decrypt. Cannot be used with the --encryptedstring option.")]
        public string EncryptedTextFile { get; set; }
    }
}
