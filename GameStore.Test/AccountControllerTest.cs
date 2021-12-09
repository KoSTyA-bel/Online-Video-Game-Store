using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Controllers;
using GameStore.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace GameStore.Test
{
    [TestFixture]
    class AccountControllerTest
    {
        public static IEnumerable<(string login, string password)> GetUserLogins
        {
            get
            {
                yield return ("aaaa", "111111");
                yield return ("bbbb", "111111");
                yield return ("cccc", "111111");
                yield return ("dddd", "111111");
                yield return ("eeee", "111111");
                yield return ("ffff", "111111");
                yield return ("gggg", "111111");
                yield return ("kkkk", "111111");
                yield return ("llll", "111111");
                yield return ("abcd", "111111");
                yield return ("dcba", "111111");
            }
        }

        private AccountController _controller;

        [OneTimeSetUp]
        public void SetUp()
        {
            DbContextOptionsBuilder<UserContext> builder = new();
            builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Test;Trusted_Connection=True;");
            _controller = new(new UserService(new UserContext(builder.Options)), new AccountValidator());
            _controller.Ok();
        }

        [Test]
        public void HttpGetLoginTest()
        {
            Assert.AreEqual(typeof(ViewResult) , _controller.Login().GetType());
        }

        [Test]
        public void HttpGetRegistrTest()
        {
            Assert.AreEqual(typeof(ViewResult), _controller.Register().GetType());
        }

        [Test]
        public void HttpGetLogoutTest()
        {
            Assert.AreEqual(typeof(Task<IActionResult>), _controller.Logout().GetType());
        }

        [TestCaseSource(nameof(GetUserLogins))]
        public void HttpPostLoginRedirectTest((string login, string password) data)
        {
            var res = _controller.Login(new Models.LoginModel() { Login = data.login, Password = string.Empty }, null) as Task<IActionResult>;
            Assert.NotNull(res);
        }

        [TestCaseSource(nameof(GetUserLogins))]
        public void HttpPostLoginViewTest((string login, string password) data)
        {
            Assert.AreEqual(typeof(Task<IActionResult>), _controller.Login(new Models.LoginModel() { Login = data.login, Password = string.Empty }, null).GetType());
        }

        [TestCaseSource(nameof(GetUserLogins))]
        public void HttpPostRegistrRedirectTest((string login, string password) data)
        {
            var res = _controller.Login(new Models.RegisterModel() { Login = data.login, Password = string.Empty, ConfirmPassword = string.Empty }, null);
            Assert.NotNull(res);
        }

        [TestCaseSource(nameof(GetUserLogins))]
        public void HttpPostRegistrViewTest((string login, string password) data)
        {
            var res = _controller.Login(new Models.RegisterModel() { Login = data.login, Password = data.password, ConfirmPassword = data.password }, null);
            Assert.NotNull(res);
        }
    }
}
