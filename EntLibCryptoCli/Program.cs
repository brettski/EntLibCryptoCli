using System;
using CommandLine;

namespace EntLibCryptoCli
{
    class Program
    {
        static int Main(string[] args)
        {
            return Parser.Default.ParseArguments<Options.GenerateKey, Options.RestoreKey, Options.ArchiveKey, Options.Encrypt, Options.Decrypt>(args)
                .MapResult(
                (Options.GenerateKey opts) => Service.GenerateKeyService.RunGenerate(opts),
                (Options.RestoreKey opts) => Service.RestoreKeyService.RunRestore(opts),
                (Options.ArchiveKey opts) => Service.ArchiveKeyService.RunArchive(opts),
                (Options.Encrypt opts) => Service.EncryptService.RunEncrypt(opts),
                (Options.Decrypt opts) => Service.DecryptService.RunDecrypt(opts),
                errs => 1000);
        }
    }
}
