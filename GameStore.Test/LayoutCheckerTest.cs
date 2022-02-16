using System;
using GameStore.Models;
using NUnit.Framework;

namespace GameStore.Test
{
    [TestFixture]
    public class LayoutCheckerTest
    {
        [TestCase(true, ExpectedResult = "_Authorize")]
        [TestCase(false, ExpectedResult = "_NonAuthorize")]
        public string GetLayoutTest(bool boolean) => LayoutChecker.GetLayout(boolean);

        [TestCase(ExpectedResult = "_Layout")]
        public string GetLayoutTest() => LayoutChecker.GetLayout();
    }
}
