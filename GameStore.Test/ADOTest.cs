using System;
using System.Linq;
using GameStore.Models.Users;
using NUnit.Framework;

namespace GameStore.Test
{
    [TestFixture]
    public class ADOTest
    {
        //[Test]
        //public void InseartUser()
        //{
        //    var ado = new UserContextADO("Server=(localdb)\\mssqllocaldb;Database=UsersData;Trusted_Connection=True;");
        //    Assert.AreEqual(8, ado.AddUser(new User() { Login = "froggg", Password = "superfrog", RoleId = 2 }));
        //}

        //[Test]
        //public void UpdateUser()
        //{
        //    var ado = new UserContextADO("Server=(localdb)\\mssqllocaldb;Database=UsersData;Trusted_Connection=True;");
        //    ado.UpdateUser(new User() { Id =  8, Login = "froger", Password = "frogsuper", RoleId = 2 });
        //    Assert.AreEqual(8, 8);
        //}

        //[Test]
        //public void DeleteUser()
        //{
        //    var ado = new UserContextADO("Server=(localdb)\\mssqllocaldb;Database=UsersData;Trusted_Connection=True;");
        //    ado.DeleteUser(7);
        //    Assert.AreEqual(8, 8);
        //}

        [Test]
        public void SelectUser()
        {
            var ado = new UserContextADO("Server=(localdb)\\mssqllocaldb;Database=UsersData;Trusted_Connection=True;");
            var user = ado.SelectUser(7);
            Assert.Null(user);

            user = ado.SelectUser(8);
            Assert.NotNull(user);
            Assert.AreEqual(8, user.Id);
            Assert.AreEqual("froger", user.Login);
            Assert.AreEqual("frogsuper", user.Password);
            Assert.AreEqual(2, user.RoleId);
        }

        [Test]
        public void GetAllUsers()
        {
            var ado = new UserContextADO("Server=(localdb)\\mssqllocaldb;Database=UsersData;Trusted_Connection=True;");
            var users = ado.GetAllUsers();
            Assert.AreEqual(7, users.Count());
        }

        [Test]
        public void GetCountOfUsers()
        {
            var ado = new UserContextADO("Server=(localdb)\\mssqllocaldb;Database=UsersData;Trusted_Connection=True;");
            var count = ado.GetCountOfUsers();
            Assert.AreEqual(7, count);
        }
    }
}
