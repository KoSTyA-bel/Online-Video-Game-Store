using System;
using System.Linq;
using System.Collections.Generic;
using GameStore.Models;
using GameStore.Models.Cart;
using NUnit.Framework;

namespace GameStore.Test
{
    [TestFixture]
    class CartTest
    {
        private Cart cart = new Cart();

        private static IEnumerable<Product> GetProducts()
        {
            yield return new Product();
            yield return new Product() { Id = 1 };
            yield return new Product() { Id = 35, Name = "ПЕСЕН" };
            yield return new Product() { Id = 58, Name = "ЕЩЁ", Description = "НЕНАПИСАНЫХ" };
            yield return new Product() { Id = 964, Name = "СКОЛЬКО", Description = "СКАЖИ", PathToPicture = "КУКУШКААААА" };
            yield return new Product() { Id = 3, Name = "ПРО", Description = "ПО", PathToPicture = "Й", Price = 2 };
            yield return new Product() { Name = "Cyberpunk 2077" };
            yield return new Product() { Price = 256.65m };
        }

        [Order(1)]
        [TestCaseSource(nameof(GetProducts))]
        public void AddProductToCartTrueTest(Product product)
        {
            Assert.IsTrue(cart.AddProductToCart(product));
            var productInCart = cart.CartLines.Where(x => x.Product == product).FirstOrDefault();
            Assert.NotNull(productInCart);
            Assert.AreEqual(product, productInCart.Product);
        }

        [Order(2)]
        [Test]
        public void AddProductToCartFalseTest()
        {
            Assert.IsFalse(cart.AddProductToCart(null));
        }

        [Order(3)]
        [TestCaseSource(nameof(GetProducts))]
        public void RemoveProductFromCartTrueTest(Product product)
        {
            Assert.IsTrue(cart.RemoveProductFromCart(product));
            var productInCart = cart.CartLines.Where(x => x.Product == product).FirstOrDefault();
            Assert.Null(productInCart);
        }

        [Order(4)]
        [Test]
        public void RemoveProductFromCartFalseTest()
        {
            Assert.IsFalse(cart.RemoveProductFromCart(null));
        }

        [Order(5)]
        [TestCaseSource(nameof(GetProducts))]
        public void RemoveProductFromCartFalseTest(Product product)
        {
            Assert.IsFalse(cart.RemoveProductFromCart(product));
        }
    }
}
