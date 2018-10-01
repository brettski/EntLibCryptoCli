using CommandLine;

namespace EntLibCryptoCli.Options
{
    [Verb("restorekey", HelpText = "Restores a key on a machine by importing a previously exported provider key.")]
    public class RestoreKey
    {

        [Option('i', "importfile", Required = true, HelpText = "Previously exported key to be imported")]
        public string ImportFile { get; set; }

        [Option('k', "keyoutput", Required = true, HelpText = "New symmetric Key file to save")]
        public string OutputKeyFile { get; set; }

        [Option('p', "password", Required = true, HelpText = "Password used to secure previously exported key")]
        public string Password { get; set; }
    }
}
