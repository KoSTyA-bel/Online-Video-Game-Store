using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Services.Products
{
    public interface IProductServiceAsync : IProductService
    {
        public Task<bool> AddProductAsync(Product product);

        public Task<bool> CreateProductAsync(string name, string description, decimal price, string pathToPicture);

        public Task<IEnumerable<Product>> GetAllProductsAsync();

        public Task RemoveProductAsync(Product product);

        public Task UpdateProductAsync(Product product);

        public Task<Product> GetLastProductAsync();
    }
}
