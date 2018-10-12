using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EntLibCryptoCli.Utilities;

namespace EntLibCryptoCli.Tests
{
    [TestClass]
    public class ValidatePassword
    {
        [TestMethod]
        public void PasswordLenthLessThan8IsFailedEqTrue()
        {
            ValidatePasswordResult result = Validate.Password("qwe");
            Assert.IsTrue(result.TestIsFailed[(int)PasswordTestResult.Length]);
        }

        [TestMethod]
        public void PasswordLenthOf8IsFailedEqFalse()
        {
            ValidatePasswordResult result = Validate.Password("abcdefgh");
            Assert.IsFalse(result.TestIsFailed[(int)PasswordTestResult.Length]);
        }

        [TestMethod]
        public void PasswordWithNoSpecialCharsIsFailedEqTrue()
        {
            ValidatePasswordResult result = Validate.Password("abcdefghijklmnopq");
            Assert.IsTrue(result.TestIsFailed[(int)PasswordTestResult.CharSpecial]);
        }

        [TestMethod]
        public void PasswordWith1SpecialCharsIsFailedEqFalse()
        {
            ValidatePasswordResult result = Validate.Password("abcdefgh!jklmnopq");
            Assert.IsFalse(result.TestIsFailed[(int)PasswordTestResult.CharSpecial]);
        }

        [TestMethod]
        public void PasswordWithNoCapitalCharsIsFailedEqTrue()
        {
            ValidatePasswordResult result = Validate.Password("abcdefghijklmnopq");
            Assert.IsTrue(result.TestIsFailed[(int)PasswordTestResult.Charcase]);
        }

        [TestMethod]
        public void PasswordWith1CapitalCharsIsFailedEqFalse()
        {
            ValidatePasswordResult result = Validate.Password("abcdefghiJklmnopq");
            Assert.IsFalse(result.TestIsFailed[(int)PasswordTestResult.Charcase]);
        }

        [TestMethod]
        public void ValidatePasswordResultIsFailedEqTrueWithInvalidPassword()
        {
            ValidatePasswordResult result = Validate.Password("abcdefghiJklmnopq");
            Assert.IsTrue(result.IsFailed);
        }

        [TestMethod]
        public void ValidatePasswordResultMessageHasLengthWithInvalidPassword()
        {
            ValidatePasswordResult result = Validate.Password("abcdefghiJklmnopq");
            Assert.IsTrue(result.FailedReason.Length > 10);
        }

        [TestMethod]
        public void ValidatePasswordResultIsFailedEqFalseWithValidPassword()
        {
            ValidatePasswordResult result = Validate.Password("abcdefghiJ#klmnopq");
            Assert.IsFalse(result.IsFailed);
        }

        [TestMethod]
        public void ValidatePasswordResultMessageEmptyWithValidPassword()
        {
            ValidatePasswordResult result = Validate.Password("abcdefghiJ#klmnopq");
            Assert.IsTrue(result.FailedReason.Length == 0);
        }
    }
}
