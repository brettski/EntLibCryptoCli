using CommandLine;

namespace EntLibCryptoCli.Options
{
    [Verb("export", HelpText = "Export a current symmetric key so it may be transferred to a new computer.")]
    public class Export
    {
        [Option('k', "keyfile", Required = true, HelpText = "Current working Symmetric key to export")]
        public string KeyFile { get; set; }

        [Option('p', "password", Required = true, HelpText = "Password to protext exported key. Min 8, 1 punctuation")]
        public string Password { get; set; }

        [Option('e', "exportkey", Required = true, HelpText = "Location and name for exported key")]
        public string ExportKey { get; set; }
    }
}
