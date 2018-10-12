using System;
using System.Text;
using System.Text.RegularExpressions;

namespace EntLibCryptoCli.Utilities
{
    public static class Validate
    {

        public static ValidatePasswordResult Password (string password)
        {
            bool isFailed = false;
            StringBuilder ReasonFailed = new StringBuilder();

            if (password.Length < 8)
            {
                isFailed = true;
                ReasonFailed.AppendLine("- Must be greater than 8 characters");
            }
            
            if (!(Regex.Match(password, @"[a-z]").Success && Regex.Match(password, @"[A-Z]").Success))
            {
                isFailed = true;
                ReasonFailed.AppendLine("- Must contain both upper and lower case characters");
            }

            string specialChars = @"!,@,#,$,%,^,&,*,?,_,~,-,(,)";
            if (!(Regex.Match(password, $"[{specialChars}]").Success))
            {
                isFailed = true;
                ReasonFailed.AppendLine($"- Must contain at least 1 special character: {specialChars}");
            }

            return new ValidatePasswordResult
            {
                IsFailed = isFailed,
                FailedReason = ReasonFailed.ToString()
            };
        }

    }
}
