using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GameStore
{
    /// <summary>
    /// The class is used to check the login and password for compliance with certain criteria.
    /// </summary>
    public sealed class AccountValidator
    {
        private static readonly string _loginPattern = "^([A-Za-z0-9_]{4,20})$";
        private static readonly string _passwordPattern = "^(.{6,20})$";

        public bool VerifyData(string login, string password, string confirmPassword) => VerifyLogin(login) && VerifyPassword(password) && VerifyConfirmPassword(password, confirmPassword);

        /// <summary>
        /// Checks if the login meets the specified standards(length and symbols).
        /// </summary>
        /// <param name="login"> Login to check.</param>
        /// <param name="message"> Verification status message.</param>
        /// <returns> If the login is normal then returns true, false if all other cases.</returns>
        /// <exception cref="ArgumentNullException"> Thrown when <paramref name="login"/> is null.</exception>
        public bool VerifyLogin(string login)
        {
            if (login is null)
            {
                throw new ArgumentNullException(nameof(login));
            }

            return MathcString(login, _loginPattern);
        }

        /// <summary>
        /// Checks whether the password meets the specified requirements.
        /// </summary>
        /// <param name="password"> Password for verification.</param>
        /// <param name="message"> Verification status message.</param>
        /// <returns> If the password is normal then returns true, false if all other cases.</returns>
        /// <exception cref="ArgumentNullException"> Thrown when  <paramref name="password"/> is null.</exception>
        public bool VerifyPassword(string password)
        {
            if (password is null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            return MathcString(password, _passwordPattern);
        }

        /// <summary>
        /// Checks if passwords match.
        /// </summary>
        /// <param name="password"> Password for verification.</param>
        /// <param name="confirmpassword"> Confirm password for verification.</param>
        /// <param name="message"> Verification status message.</param>
        /// <returns> True if the passwords match, false in all other situations.</returns>
        /// <exception cref="ArgumentNullException"> Thrown when <paramref name="password"/> or <paramref name="confirmpassword"/> is null.</exception>
        public bool VerifyConfirmPassword(string password, string confirmpassword)
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

            return result;
        }

        private static bool MathcString(string data, string pattern) => Regex.IsMatch(data, pattern, RegexOptions.Compiled);
    }
}
