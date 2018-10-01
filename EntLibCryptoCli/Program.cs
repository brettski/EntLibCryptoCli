using System;
using CommandLine;

namespace EntLibCryptoCli
{
    class Program
    {
        static int Main(string[] args)
        {
            return Parser.Default.ParseArguments<Options.RestoreKey, Options.ExportKey>(args)
                .MapResult(
                (Options.RestoreKey opts) => Service.RestoreKeyService.RunRestore(opts),
                (Options.ExportKey opts) => Service.RestoreKeyService.RunRestore(new Options.RestoreKey()),
                errs => 1000);

            /*
            var result = Parser.Default.ParseArguments<Options.Import, Options.Export>(args);
            Console.WriteLine(result);
            if (ParserResultType.Parsed == result.Tag)
            {
                //result = {CommandLine.Parsed<object>}
                
                Console.WriteLine("parsed");
                result.WithParsed<Options.Import>(opts =>
                {
                    Console.WriteLine($"Import keyfile: {opts.ImportFile}");    
                });
            }
            */
            // Else if falls out showing help
        }
    }
}
