using System;
using NUnit.Framework;
using GameStore.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Test
{
    [TestFixture]
    public class ProductServiceTest
    {
        private static DbContextOptionsBuilder<ProductContext> builder = new DbContextOptionsBuilder<ProductContext>();
        private static ProductService _service = new ProductService(new ProductContext(builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Test;Trusted_Connection=True;").Options));

        [TestCase("Game", ExpectedResult = true)]
        [TestCase("Game", ExpectedResult = false)]
        public bool CreateProductTest(string name) => _service.CreateProduct(name, string.Empty, 0, string.Empty);

        [TestCase("Game", ExpectedResult = true)]
        [TestCase("Game", ExpectedResult = false)]
        public bool RemoveProductTest(string name) => _service.RemoveProduct(new Product()
        {
            Id = 1,
            Name = name,
            Description = string.Empty,
            Price = 0,
            PathToPicture = string.Empty,
        });
    }
}
