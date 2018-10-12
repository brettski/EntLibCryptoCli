using CommandLine;

namespace EntLibCryptoCli.Options
{
    [Verb("restorekey", HelpText = "Restores a key on a machine by importing a previously exported provider key.")]
    public class RestoreKey
    {

        [Option('a', "archivefile", Required = true, HelpText = "Previously archived key to be imported and restored to keyfile")]
        public string ArchiveFile { get; set; }

        [Option('k', "keyfile", Required = true, HelpText = "New symmetric Key file to restore")]
        public string OutputKeyFile { get; set; }

        [Option('p', "password", Required = true, HelpText = "Password used to secure archive file")]
        public string Password { get; set; }
    }
}
