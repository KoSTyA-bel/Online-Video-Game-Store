using System;
using GameStore.Services.Products;
using System.Reflection;
using NUnit.Framework;
using System.Collections.Generic;

namespace GameStore.Test
{
    [TestFixture]
    public class ProductTest
    {
        private static IEnumerable<(Product first, Product second)> GetProducts {
            get
            {
                yield return (new Product(), new Product());
                yield return (new Product() { Id = 1 }, new Product() { Id = 1 });
                yield return (new Product() { Id = 1, Name = "abc" }, new Product() { Id = 1, Name = "abc" });
                yield return (new Product() { Name = "abc" }, new Product() { Name = "abc" });
                yield return (new Product() { Name = "abc", Description = "asdasdhjasdkfsak" }, new Product() { Name = "abc", Description = "asdasdhjasdkfsak" });
            }
        }

        [TestCase("Id", typeof(int))]
        [TestCase("Name", typeof(string))]
        [TestCase("Description", typeof(string))]
        [TestCase("Price", typeof(decimal))]
        [TestCase("PathToPicture", typeof(string))]
        public void TestPublicInstanceProperty(string name, Type type)
        { 
            var propertyInfo = typeof(Product).GetProperty(name, BindingFlags.Public | BindingFlags.Instance);
            Assert.AreEqual(name, propertyInfo.Name);
            Assert.AreEqual(type, propertyInfo.PropertyType);
        }

        [TestCaseSource(nameof(GetProducts))]
        public void EqualsTrueTest((Product first, Product second) products)
        {
            Assert.AreEqual(products.first, products.second);
        }

        [TestCaseSource(nameof(GetProducts))]
        public void EqualsFalseTest((Product first, Product second) products)
        {
            products.second.Id = -20;
            Assert.AreNotEqual(products.first, products.second);
        }
    }
}
