using System;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using GameStore.Services.Users;

namespace GameStore.Test
{
    [TestFixture]
    class IUserServiceTest
    {
        [TestCase("GetUser", typeof(User))]
        [TestCase("ContainsUser", typeof(bool))]
        [TestCase("RegistrAdmin", typeof(int))]
        [TestCase("RegistrUser", typeof(int))]
        [TestCase("TryGetRole", typeof(Role))]
        public void CheckingForMethods(string name, Type type)
        {
            var methodInfo = typeof(IUserService).GetMethod(name);
            Assert.AreEqual(type, methodInfo.ReturnType);
        }

        [Test]
        public void CheckingTheOperabilityOfMethods()
        {
            var mock = new Mock<IUserService>();
            mock.Setup(x => x.ContainsUser(It.IsAny<string>())).Returns(true);
            mock.Setup(x => x.RegistrAdmin(It.IsAny<string>(), It.IsAny<string>())).Returns(() => { var u = new User();
                return u.Id;
            });
            mock.Setup(x => x.RegistrUser(It.IsAny<string>(), It.IsAny<string>())).Returns(() => {
                var u = new User();
                return u.Id;
            });
            mock.Setup(x => x.TryGetRole(It.IsAny<int?>())).Returns(new Role());

            var concreateService = mock.Object;

            Assert.NotNull(concreateService.ContainsUser(null));
            Assert.NotNull(concreateService.RegistrAdmin(null, null));
            Assert.NotNull(concreateService.RegistrUser(null, null));
            Assert.NotNull(concreateService.TryGetRole(null));
        }

        [Test]
        public void CheckingMethodInvocation()
        {
            var mock = new Mock<IUserService>();
            mock.Setup(x => x.ContainsUser(It.IsAny<string>())).Returns(true);
            mock.Setup(x => x.RegistrAdmin(It.IsAny<string>(), It.IsAny<string>())).Returns(1);
            mock.Setup(x => x.RegistrUser(It.IsAny<string>(), It.IsAny<string>())).Returns(2);
            mock.Setup(x => x.TryGetRole(It.IsAny<int?>())).Returns(new Role());

            var concreateService = new WorkWithUserServicePlug(mock.Object);

            concreateService.Find();
            concreateService.RegA();
            concreateService.RegU();
            concreateService.TryGetRole();

            mock.VerifyAll();
        }
    }
}
