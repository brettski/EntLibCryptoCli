using System;
using CommandLine;

namespace EntLibCryptoCli
{
    class Program
    {
        static int Main(string[] args)
        {
            return Parser.Default.ParseArguments<Options.RestoreKey, Options.ArchiveKey, Options.Encrypt>(args)
                .MapResult(
                (Options.RestoreKey opts) => Service.RestoreKeyService.RunRestore(opts),
                (Options.ArchiveKey opts) => Service.ArchiveKeyService.RunArchive(opts),
                (Options.Encrypt opts) => Service.EncryptService.RunEncrypt(opts),
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
