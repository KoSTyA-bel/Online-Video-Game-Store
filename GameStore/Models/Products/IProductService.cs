using System.Collections.Generic;

namespace GameStore.Models
{
    public interface IProductService
    {
        bool AddProduct(Product product);

        IEnumerable<Product> GetAllProducts();

        bool RemoveProduct(Product product);

        bool TryShowProduct(int id, out Product product);

        bool UpdateProduct(Product product);
    }
}