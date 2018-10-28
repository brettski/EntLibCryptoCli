using CommandLine;

namespace EntLibCryptoCli.Options
{
    [Verb("encrypt", HelpText = "Encrypt text from command line or file")]
    public class Encrypt
    {
        [Option('s', "textstring", SetName = "text", Required = true, HelpText = "A string of text to encrypt. Cannot be used with --textfile option.")]
        public string PlainTextString { get; set; }
        
        [Option('f', "textfile", SetName = "file", Required = true, HelpText = "A file of plain text to encyrpt. Cannot be used with --textstring option.")]
        public string PlanTextFile { get; set; }
        /*
        [Option('o', "outputfile", Required = false, HelpText ="(Optional) Outputs encrypted text to this file instead of console.")]
        public string EncryptTextFile { get; set; }
        */
    }
}
