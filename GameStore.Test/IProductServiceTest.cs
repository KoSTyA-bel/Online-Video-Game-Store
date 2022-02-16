using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using GameStore.Services.Products;
using System.Threading.Tasks;

namespace GameStore.Test
{
    [TestFixture]
    class IProductServiceTest
    {
        [TestCase("AddProduct", typeof(bool))]
        [TestCase("CreateProduct", typeof(bool))]
        [TestCase("GetAllProducts", typeof(IEnumerable<Product>))]
        [TestCase("RemoveProduct", typeof(bool))]
        [TestCase("TryShowProduct", typeof(bool))]
        [TestCase("UpdateProduct", typeof(bool))]
        [TestCase("GetLastProduct", typeof(Product))]
        public void CheckingForMethods(string name, Type type)
        {
            var methodInfo = typeof(IProductService).GetMethod(name);
            Assert.AreEqual(type, methodInfo.ReturnType);
        }

        [Test]
        public void CheckingTheOperabilityOfMethods()
        {
            var product = new Product();
            var mock = new Mock<IProductService>();
            mock.Setup(x => x.AddProduct(It.IsAny<Product>())).Returns<Product>(x => true);
            mock.Setup(x => x.RemoveProduct(It.IsAny<Product>())).Returns<Product>(x => true);
            mock.Setup(x => x.UpdateProduct(It.IsAny<Product>())).Returns<Product>(x => true);

            var concreateService = mock.Object;

            Assert.AreEqual(true, concreateService.AddProduct(product));
            Assert.AreEqual(true, concreateService.RemoveProduct(product));
            Assert.AreEqual(true, concreateService.UpdateProduct(product));
        }

        [Test]
        public void CheckingMethodInvocation()
        {
            var mock = new Mock<IProductService>();
            mock.Setup(x => x.AddProduct(It.IsAny<Product>())).Returns<Product>(x => true);
            mock.Setup(x => x.RemoveProduct(It.IsAny<Product>())).Returns<Product>(x => true);
            mock.Setup(x => x.UpdateProduct(It.IsAny<Product>())).Returns<Product>(x => true);

            var tester = new WorkWithProductServicePlug(mock.Object);

            tester.Create();
            tester.Remove();
            tester.Update();

            mock.Verify(x => x.AddProduct(It.IsAny<Product>()), Times.Once());
            mock.Verify(x => x.RemoveProduct(It.IsAny<Product>()), Times.Once());
            mock.Verify(x => x.UpdateProduct(It.IsAny<Product>()), Times.Once());
        }
    }
}
