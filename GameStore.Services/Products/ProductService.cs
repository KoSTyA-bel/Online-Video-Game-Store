using System;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Services.Products
{
    /// <summary>
    /// A service for working with products that implements the interface <see cref="IProductService"/>.
    /// </summary>
    public class ProductService : IProductService
    {
        private IProductContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        /// <param name="context">Database context.</param>
        public ProductService(IProductContext context)
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

            _context.AddProduct(product);

            return true;
        }

        /// <inheritdoc/>
        public IEnumerable<Product> GetAllProducts() => _context.GetAllProducts();

        /// <inheritdoc/>
        public bool RemoveProduct(Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            _context.DeleteProduct(product.Id);

            return true;
        }

        /// <inheritdoc/>
        public bool UpdateProduct(Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));

                // return false;
            }

            _context.UpdateProduct(product);

            return true;
        }

        /// <inheritdoc/>
        public bool TryShowProduct(int id, out Product product)
        {
            product = _context.SelectProduct(id);

            return !(product is null);
        }

        /// <inheritdoc/>
        public bool CreateProduct(string name, string description, decimal price, string pathToPicture)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (description is null)
            {
                throw new ArgumentNullException(nameof(description));
            }

            if (pathToPicture is null)
            {
                throw new ArgumentNullException(nameof(pathToPicture));
            }

            var product = new Product()
            {
                Name = name,
                Description = description,
                Price = price,
                PathToPicture = pathToPicture,
            };

            _context.AddProduct(product);

            return true;
        }

        /// <inheritdoc/>
        public Product GetLastProduct() => _context.GetAllProducts().LastOrDefault();
    }
}
