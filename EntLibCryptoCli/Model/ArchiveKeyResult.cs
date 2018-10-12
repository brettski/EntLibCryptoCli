using System;


namespace EntLibCryptoCli.Model
{
    public class ArchiveKeyResult
    {
        public string SecuredKey { get; set; }

        public string ArchiveKey { get; set; }

        public string Password { get; set; }

        public bool IsInError { get; set; }

        public string ErrorString { get; set; }

        public Exception Exception { get; set; }
    }
}
