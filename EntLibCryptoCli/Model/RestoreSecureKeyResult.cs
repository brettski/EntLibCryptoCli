using System;

namespace EntLibCryptoCli.Model
{
    public class RestoreSecureKeyResult
    {
        public string ImportFile { get; set; }

        public string RestoredKey { get; set; }

        public string Password { get; set; }

        public bool IsInError { get; set; }

        public string ErrorString { get; set; }

        public Exception Exception { get; set; }
    }
}
