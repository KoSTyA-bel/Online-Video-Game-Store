using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Reflection;
using GameStore.Models.Users;

namespace GameStore.Test
{
    [TestFixture]
    public class UserTest
    {
        [TestCase("Id", typeof(int))]
        [TestCase("Login", typeof(string))]
        [TestCase("Password", typeof(string))]
        [TestCase("Role", typeof(Role))]
        [TestCase("RoleId", typeof(int?))]
        public void TestPublicInstanceProperty(string name, Type type)
        {
            var propertyInfo = typeof(User).GetProperty(name, BindingFlags.Public | BindingFlags.Instance);
            Assert.AreEqual(name, propertyInfo.Name);
            Assert.AreEqual(type, propertyInfo.PropertyType);
        }
    }
}
