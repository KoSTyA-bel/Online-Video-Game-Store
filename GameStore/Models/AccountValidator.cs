using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesForGameStore
{
    public sealed class AccountValidator
    {
        private static IEnumerable<char> validChars = new char[]
        {
            'A',
            'B',
            'C',
            'D',
            'E',
            'F',
            'G',
            'H',
            'I',
            'J',
            'K',
            'L',
            'M',
            'N',
            'O',
            'P',
            'Q',
            'R',
            'S',
            'T',
            'U',
            'V',
            'W',
            'X',
            'Y',
            'Z',
            '_',
        };
        private IReport _reporter;

        public AccountValidator(IReport reporter)
        {
            _reporter = reporter ?? throw new ArgumentNullException(nameof(reporter));
        }

        public IReport Reporter => _reporter;

        /// <summary>
        /// Checks if the login meets the specified standards(length and symbols).
        /// </summary>
        /// <param name="login"> Login to check.</param>
        /// <param name="message"> Verification status message.</param>
        /// <returns> If the login is normal then returns true, false if all other cases.</returns>
        /// <exception cref="ArgumentNullException"> Thrown when <paramref name="login"/> is null.</exception>
        public bool VerifyLogin(string login, out string message)
        {
            if (login is null)
            {
                throw new ArgumentNullException(nameof(login));
            }

            message = string.Empty;
            bool result = false;

            if (login.Length < 4)
            {
                message = _reporter.ShortLogin;
            }
            else if (login.Length > 20)
            {
                message = _reporter.LongLogin;
            }
            else
            {
                int correctSymbols = 0;

                foreach (var symbol in login.ToUpper())
                {
                    if (!validChars.Contains(symbol))
                    {
                        message = _reporter.InvalidSymbols;
                        break;
                    }

                    correctSymbols++;
                }

                if (correctSymbols == login.Length)
                {
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// Checks whether the password meets the specified requirements.
        /// </summary>
        /// <param name="password"> Password for verification.</param>
        /// <param name="message"> Verification status message.</param>
        /// <returns> If the password is normal then returns true, false if all other cases.</returns>
        /// <exception cref="ArgumentNullException"> Thrown when  <paramref name="password"/> is null.</exception>
        public bool VerifyPassword(string password, out string message)
        {
            if (password is null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            message = string.Empty;
            bool result = false;

            if (password.Length < 6)
            {
                message = _reporter.ShortPassword;
            }
            else if (password.Length > 20)
            {
                message = _reporter.LongPassword;
            }
            else
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Checks if passwords match.
        /// </summary>
        /// <param name="password"> Password for verification.</param>
        /// <param name="confirmpassword"> Confirm password for verification.</param>
        /// <param name="message"> Verification status message.</param>
        /// <returns> True if the passwords match, false in all other situations.</returns>
        /// <exception cref="ArgumentNullException"> Thrown when <paramref name="password"/> or <paramref name="confirmpassword"/> is null.</exception>
        public bool VerifyConfirmPassword(string password, string confirmpassword, out string message)
        {
            if (password is null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            if (confirmpassword is null)
            {
                throw new ArgumentNullException(nameof(confirmpassword));
            }

            bool result = password.Equals(confirmpassword);
            message = result ? string.Empty : _reporter.PasswordsDontMacth;

            return result;
        }
    }
}
