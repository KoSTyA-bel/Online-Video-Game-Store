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
        private AccountValidator validator = new AccountValidator();

        #region PasswordVerify
        [Test]
        public void PasswordVerifyThrownArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => this.validator.VerifyPassword(null));
        }

        [TestCase("")]
        [TestCase("asdfghjkl;'qwertyuiop[zxcvbnm,./1234567890-=")]
        public void PasswordVerifyFalseResultTest(string password)
        {
            Assert.AreEqual(false, this.validator.VerifyPassword(password));
        }

        [TestCase("1234567")]
        public void PasswordVerifyTrueResultTest(string password)
        {
            Assert.AreEqual(true, this.validator.VerifyPassword(password));
        }
        #endregion

        #region LoginVerify
        [Test]
        public void LoginVerifyThrownArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => this.validator.VerifyLogin(null));
        }

        [TestCase("")]
        [TestCase("1234567890asdfghjkl;qwertyuiop")]
        [TestCase(" 123")]
        [TestCase("1_ж3 ")]
        public void LoginVerifyFalseTest(string login)
        {
            Assert.AreEqual(false, this.validator.VerifyLogin(login));
        }

        [TestCase("______")]
        [TestCase("asdjDFDJdn_vsd")]
        public void LoginVerifyTrueTest(string login)
        {
            Assert.AreEqual(true, this.validator.VerifyLogin(login));
        }
        #endregion
    }
}
