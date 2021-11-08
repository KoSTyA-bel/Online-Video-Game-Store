using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Models
{
    /// <summary>
    /// Returns the master page depending on whether the user is logged in or not
    /// </summary>
    public static class LayoutChecker
    {
        /// <summary>
        /// Returns the standard master page.
        /// </summary>
        /// <returns>Standard Master page.</returns>
        public static string GetLayout() => "_Layout";

        /// <summary>
        /// Returns the master page depending on whether the user is logged in or not.
        /// </summary>
        /// <param name="isAuthorize">Is the user logged in or not.</param>
        /// <returns>Master Page.</returns>
        public static string GetLayout(bool isAuthorize) => isAuthorize ? "_Authorize" : "_NonAuthorize";
    }
}
