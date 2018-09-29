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

        /* I don't think this is needed
        [Option('h', "help", Required = false, HelpText = "Desplays help infomation.")]
        public bool Help { get; set; }
        */
    }
}
