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


        [Test]
        public void PasswordLengthVerifyTest()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < 1000; i++)
            {
                if (i >= 6 && i <= 20)
                {
                    Assert.IsTrue(validator.VerifyPassword(sb.ToString()));
                }
                else
                {
                    Assert.IsFalse(validator.VerifyPassword(sb.ToString()));
                }

                sb.Append('A');
            }
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

        [Test]
        public void LoginLengthVerifyTest()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < 1000; i++)
            {
                if (i >= 4 && i <= 20)
                {
                    Assert.IsTrue(validator.VerifyLogin(sb.ToString()));
                }
                else
                {
                    Assert.IsFalse(validator.VerifyLogin(sb.ToString()));
                }

                sb.Append('A');
            }
        }
        #endregion
    }
}
