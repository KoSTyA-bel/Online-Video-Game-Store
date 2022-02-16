using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Services.Products
{
    /// <summary>
    /// Work with DB by EntityFrameworkCore.
    /// </summary>
    public class ProductContext : DbContext, IProductContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductContext"/> class.
        /// </summary>
        /// <param name="options">Options for creating context.</param>
        public ProductContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// Gets or sets a set of data stored in the DB.
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public void DeleteProduct(int id)
        {
            var product = SelectProduct(id);

            if (product != null)
            {
                this.Products.Remove(product);
                SaveChanges();
            }
        }

        /// <inheritdoc/>
        public IEnumerable<Product> GetAllProducts() => this.Products;

        /// <inheritdoc/>
        public int GetCountOfProducts() => this.Products.Count();

        /// <inheritdoc/>
        public Product SelectProduct(int id) => this.Products.Where(product => product.Id == id).FirstOrDefault();

        /// <inheritdoc/>
        public void UpdateProduct(Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            this.Products.Update(product);

            this.Products.Update(product);

            this.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(u => u.Id);
        }
    }
}
