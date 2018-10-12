using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntLibCryptoCli.Utilities
{
    public struct ValidatePasswordResult
    {
        public bool IsFailed { get; set; }

        public string FailedReason { get; set; }
    }
}
