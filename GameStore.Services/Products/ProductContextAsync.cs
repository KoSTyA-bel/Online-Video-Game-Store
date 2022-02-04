using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Services.Products
{
    /// <summary>
    ///  Work with DB by EntityFrameworkCore by using asynchronyus methods.
    /// </summary>
    public class ProductContextAsync : ProductContext, IProductContextAsync
    {
        /// <summary>
        /// Creates a new instanse of class <see cref=">ProductContextAsync"/>.
        /// </summary>
        /// <param name="options">Database context options.</param>
        public ProductContextAsync(DbContextOptions options) : base(options)
        {
        }

        /// <inheritdoc/>
        public async Task<int> AddProductAsync(Product product)
        {
            await this.Products.AddAsync(product);
            var last = await this.Products.LastOrDefaultAsync();
            return last is null ? -1: last.Id;
        }

        /// <inheritdoc/>
        public async Task DeleteProductAsync(int id)
        {
            var product = await this.Products.Where(x => x.Id == id).FirstOrDefaultAsync();
            
            if (product != null)
            {
                this.Products.Remove(product);
            }
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Product>> GetAllProductsAsync() => await this.Products.ToListAsync();

        /// <inheritdoc/>
        public async Task<int> GetCountOfProductsAsync() => await this.Products.CountAsync();

        /// <inheritdoc/>
        public Task UpdateProductAsync(Product product) => Task.Factory.StartNew(() => this.Products.Update(product));
    }
}
