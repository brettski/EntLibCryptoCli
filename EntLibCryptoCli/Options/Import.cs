using CommandLine;

namespace EntLibCryptoCli.Options
{
    [Verb("import", HelpText = "Import a previously exported provider key.")]
    public class ImportOptions
    {

        [Option('i', "importfile", Required = true, HelpText = "Previously exported key to be imported")]
        public string ImportFile { get; set; }

        [Option('o', "outputkey", Required = true, HelpText = "New symmetric key file to save")]
        public string OutputKeyFile { get; set; }

        [Option('p', "password", Required = true, HelpText = "Password used to secure previously exported key")]
        public string Password { get; set; }
    }
}
