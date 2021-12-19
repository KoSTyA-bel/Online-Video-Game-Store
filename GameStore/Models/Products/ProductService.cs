using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Models
{
    /// <summary>
    /// A service for working with products that implements the interface <see cref="IProductService"/>.
    /// </summary>
    public class ProductService : IProductService
    {
        private ProductContext _context;
        private DbContextOptions options;

        /// <summary>
        /// Creates a new instance of the class <see cref="ProductService"/>.
        /// </summary>
        /// <param name="context">Database context.</param>
        public ProductService(ProductContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc/>
        public bool AddProduct(Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            if (_context.Products.Contains(product))
            {
                return false;
            }

            _context.Add(product);

            _context.SaveChanges();

            return true;
        }

        /// <inheritdoc/>
        public IEnumerable<Product> GetAllProducts() => _context.Products;

        /// <inheritdoc/>
        public bool RemoveProduct(Product product)
        {
            if (_context.Products.Contains(product))
            {
                _context.Remove(product);
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        public bool UpdateProduct(Product product)
        {
            if (product is null)
            {
                //throw new ArgumentNullException(nameof(product));
                return false;
            }

            var productToUpdate = _context.Products.Where(x => x.Id == product.Id).FirstOrDefault();

            if (productToUpdate is null)
            {
                //throw new ArgumentNullException(nameof(product));
                return false;
            }

            productToUpdate.Name = product.Name;
            productToUpdate.Description = product.Description;
            productToUpdate.Price = product.Price;

            return _context.SaveChanges() == 1;
        }

        /// <inheritdoc/>
        public bool TryShowProduct(int id, out Product product)
        {
            product = _context.Products.Where(x => x.Id == id).FirstOrDefault();

            return !(product is null);
        }

        /// <inheritdoc/>
        public bool CreateProduct(string name, string description, decimal price, string pathToPicture)
        {
            if (_context.Products.Where(p => p.Name == name).Count() != 0)
            {
                return false;
            }

            var product = new Product()
            {
                Name = name,
                Description = description,
                Price = price,
                PathToPicture = pathToPicture,
            };

            _context.Products.Add(product);

            return _context.SaveChanges() == 1;
        }

        /// <inheritdoc/>
        public Task<Product> GetLastProduct() => _context.Products.OrderBy(x => x.Id).LastAsync();
    }
}
