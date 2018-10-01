using CommandLine;

namespace EntLibCryptoCli.Options
{
    [Verb("import", HelpText = "Import a previously exported provider key.")]
    public class Import
    {

        [Option('i', "importfile", Required = true, HelpText = "Previously exported key to be imported")]
        public string ImportFile { get; set; }

        [Option('k', "outputkey", Required = true, HelpText = "New symmetric Key file to save")]
        public string OutputKeyFile { get; set; }

        [Option('p', "password", Required = true, HelpText = "Password used to secure previously exported key")]
        public string Password { get; set; }
    }
}
