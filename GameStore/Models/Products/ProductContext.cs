using GameStore.Models.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Models
{
    public class ProductContext : DbContext, IProductContext
    {
        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; }

        public int AddProduct(Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            this.Products.Add(product);
            SaveChanges();
            return product.Id;
        }

        public void DeleteProduct(int id)
        {
            var product = SelectProduct(id);

            if (product != null)
            {
                this.Products.Remove(product);
                SaveChanges();
            }
        }

        public IEnumerable<Product> GetAllProducts() => this.Products;

        public int GetCountOfProducts() => this.Products.Count();

        public Product SelectProduct(int id) => this.Products.Where(product => product.Id == id).FirstOrDefault();

        public void UpdateProduct(Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            this.Products.Update(product);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(u => u.Id);
        }
    }
}
