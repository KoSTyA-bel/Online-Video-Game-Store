using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using GameStore.Models.Users;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GameStore.Test
{
    [TestFixture]
    class UserServiceTest
    {
        private UserService _service;

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

        [OneTimeSetUp]
        public void StartUp()
        {
            DbContextOptionsBuilder<UserContext> builder = new();
            builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Test;Trusted_Connection=True;");
            _service = new(new UserContext(builder.Options));
        }

        [TestCaseSource(nameof(GetUserLogins))]
        public void CheckThatUsersInDb((string login, string password) data)
        {
            Assert.IsTrue(_service.ContainsUser(data.login).Result);
        }

        [TestCaseSource(nameof(GetUserLogins))]
        public void GetUserTrueTest((string login, string password) data)
        {
            var user = _service.GetUser(data.login).Result;

            Assert.AreEqual(data.login, user.Login);
            Assert.AreEqual(data.password, user.Password);
        }

        [TestCaseSource(nameof(GetUserLogins))]
        public void GetUserWithPasswordTrueTest((string login, string password) data)
        {
            var user = _service.GetUser(data.login, data.password).Result;

            Assert.AreEqual(data.login, user.Login);
            Assert.AreEqual(data.password, user.Password);
        }

        [Test]
        public void CheckThatServiseGetRoles()
        {
            var admin = _service.TryGetRole(1).Result;
            var user = _service.TryGetRole(2).Result;

            Assert.AreEqual("admin", admin.Name);
            Assert.AreEqual("user", user.Name);
        }
    }
}
