using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace GameStore.Test
{
    [TestFixture]
    public class AccountValidatorTest
    {
        private AccountValidator validator = new AccountValidator(new EnglishReport());

        #region PasswordVerify
        [Test]
        public void PasswordVerifyThrownArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => this.validator.VerifyPassword(null, out string message));
        }

        [TestCase("")]
        [TestCase("asdfghjkl;'qwertyuiop[zxcvbnm,./1234567890-=")]
        public void PasswordVerifyFalseResultTest(string password)
        {
            Assert.AreEqual(false, this.validator.VerifyPassword(password, out string message));
        }

        [TestCase("1234567")]
        public void PasswordVerifyTrueResultTest(string password)
        {
            Assert.AreEqual(true, this.validator.VerifyPassword(password, out string message));
        }
        #endregion

        #region LoginVerify
        [Test]
        public void LoginVerifyThrownArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => this.validator.VerifyLogin(null, out string message));
        }

        [TestCase("")]
        [TestCase("1234567890asdfghjkl;qwertyuiop")]
        [TestCase("123 ")]
        [TestCase("1_ж3 ")]
        public void LoginVerifyFalseTest(string login)
        {
            Assert.AreEqual(false, this.validator.VerifyLogin(login, out string message));
        }

        [TestCase("______")]
        [TestCase("asdjDFDJdn_vsd")]
        public void LoginVerifyTrueTest(string login)
        {
            Assert.AreEqual(true, this.validator.VerifyLogin(login, out string message));
        }
        #endregion
    }
}
