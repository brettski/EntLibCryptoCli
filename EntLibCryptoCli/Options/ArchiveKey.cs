using CommandLine;

namespace EntLibCryptoCli.Options
{
    [Verb("archivekey", HelpText = "Archives a current secured symmetric key so it may be transferred to a new computer.")]
    public class ArchiveKey
    {
        [Option('k', "keyfile", Required = true, HelpText = "Current working secured Symmetric key to archive")]
        public string KeyFile { get; set; }

        [Option('p', "password", Required = true, HelpText = "Password to protext exported key. Min 8, 1 punctuation")]
        public string Password { get; set; }

        [Option('e', "archivefile", Required = true, HelpText = "File location and name for key to export")]
        public string Archivefile { get; set; }
    }
}
