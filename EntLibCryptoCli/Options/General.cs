using CommandLine;

namespace EntLibCryptoCli.Options
{
    /// <summary>
    /// Defines command line options
    /// </summary>
    public class General
    {
        [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
        public bool Verbose { get; set; }

        [Option('h', Required = false, HelpText = "Use --help for interactive help")]
        public bool Help { get; set; }
    }
}
