using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Models
{
    public static class LayoutChecker
    {
        public static string GetLayout() => "_Layout";

        public static string GetLayout(bool isAuthorize) => isAuthorize ? "_Authorize" : "_NonAuthorize";
    }
}
