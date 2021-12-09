using System;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using GameStore.Models.Users;

namespace GameStore.Test
{
    [TestFixture]
    class IUserServiceTest
    {
        [TestCase("GetUser", typeof(Task<User>))]
        [TestCase("ContainsUser", typeof(Task<bool>))]
        [TestCase("RegistrAdmin", typeof(Task))]
        [TestCase("RegistrUser", typeof(Task))]
        [TestCase("TryGetRole", typeof(ValueTask<Role>))]
        public void CheckingForMethods(string name, Type type)
        {
            var methodInfo = typeof(IUserService).GetMethod(name);
            Assert.AreEqual(type, methodInfo.ReturnType);
        }

        [Test]
        public void CheckingTheOperabilityOfMethods()
        {
            var mock = new Mock<IUserService>();
            mock.Setup(x => x.GetUser(It.IsAny<string>(), It.IsAny<string>())).Returns(new Task<User>(x => new User(), null));
            mock.Setup(x => x.ContainsUser(It.IsAny<string>())).Returns(new Task<bool>(x => true, null));
            mock.Setup(x => x.RegistrAdmin(It.IsAny<string>(), It.IsAny<string>())).Returns(new Task(() => new User()));
            mock.Setup(x => x.RegistrUser(It.IsAny<string>(), It.IsAny<string>())).Returns(new Task(() => new User()));
            mock.Setup(x => x.TryGetRole(It.IsAny<int?>())).Returns<ValueTask<Role>>(x => new ValueTask<Role>());

            var concreateService = mock.Object;

            Assert.NotNull(concreateService.GetUser(null, null));
            Assert.NotNull(concreateService.ContainsUser(null));
            Assert.NotNull(concreateService.RegistrAdmin(null, null));
            Assert.NotNull(concreateService.RegistrUser(null, null));
            Assert.NotNull(concreateService.TryGetRole(null));
        }

        [Test]
        public void CheckingMethodInvocation()
        {
            var mock = new Mock<IUserService>();
            mock.Setup(x => x.GetUser(It.IsAny<string>(), It.IsAny<string>())).Returns(new Task<User>(x => new User(), null));
            mock.Setup(x => x.ContainsUser(It.IsAny<string>())).Returns(new Task<bool>(x => true, null));
            mock.Setup(x => x.RegistrAdmin(It.IsAny<string>(), It.IsAny<string>())).Returns(new Task(() => new User()));
            mock.Setup(x => x.RegistrUser(It.IsAny<string>(), It.IsAny<string>())).Returns(new Task(() => new User()));
            mock.Setup(x => x.TryGetRole(It.IsAny<int?>())).Returns<ValueTask<Role>>(x => new ValueTask<Role>());

            var concreateService = new WorkWithUserServicePlug(mock.Object);

            concreateService.Find();
            concreateService.RegA();
            concreateService.RegU();
            concreateService.User();
            concreateService.TryGetRole();

            mock.VerifyAll();
        }
    }
}
