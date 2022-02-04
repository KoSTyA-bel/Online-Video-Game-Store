using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Services.Products
{
    /// <summary>
    /// It represents methods that should be implemented in classes that represent working with a database of products with the ability to work asynchronously.
    /// </summary>
    public interface IProductContextAsync : IProductContext
    {
        /// <summary>
        /// Adds product to the DB.
        /// </summary>
        /// <param name="product">Product.</param>
        /// <returns>Id of the added product, or -1 in case of an error adding a product.</returns>
        public Task<int> AddProductAsync(Product product);

        /// <summary>
        /// Updates product data.
        /// </summary>
        /// <param name="product">Product whose data needs to be updated.</param>
        /// <returns>Task that will perform the product update.</returns>
        public Task UpdateProductAsync(Product product);

        /// <summary>
        /// Removes product from DB.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Task that will perform the removal of the product.</returns>
        public Task DeleteProductAsync(int id);

        /// <summary>
        /// Selects all products that contains id DB.
        /// </summary>
        /// <returns>Task that results in a list of products.</returns>
        public Task<IEnumerable<Product>> GetAllProductsAsync();

        /// <summary>
        /// Selects count of products.
        /// </summary>
        /// <returns></returns>
        public Task<int> GetCountOfProductsAsync();
    }
}
