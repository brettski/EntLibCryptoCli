using System;
using CommandLine;

namespace EntLibCryptoCli
{
    class Program
    {
        static int Main(string[] args)
        {
            return Parser.Default.ParseArguments<Options.Import, Options.Export>(args)
                .MapResult(
                (Options.Import opts) => Service.ImportService.RunImport(opts),
                (Options.Export opts) => Service.ImportService.RunImport(new Options.Import()),
                errs => 1);

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
