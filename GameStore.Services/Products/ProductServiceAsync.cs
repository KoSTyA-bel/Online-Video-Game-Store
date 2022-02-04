using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Services.Products
{
    public class ProductServiceAsync : ProductService, IProductServiceAsync
    {
        IProductContextAsync _context;

        public ProductServiceAsync(IProductContextAsync context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> AddProductAsync(Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            var id = await _context.AddProductAsync(product);

            return id == product.Id;
        }

        public async Task<bool> CreateProductAsync(string name, string description, decimal price, string pathToPicture)
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

            int id = await _context.AddProductAsync(product);

            return id == product.Id;
        }

        public Task<IEnumerable<Product>> GetAllProductsAsync() => _context.GetAllProductsAsync();

        public async Task<Product> GetLastProductAsync()
        {
            var products = await _context.GetAllProductsAsync();
            return products.FirstOrDefault();
        }

        public async Task RemoveProductAsync(Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            await _context.DeleteProductAsync(product.Id);
        }

        public async Task UpdateProductAsync(Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            await _context.UpdateProductAsync(product);
        }
    }
}
