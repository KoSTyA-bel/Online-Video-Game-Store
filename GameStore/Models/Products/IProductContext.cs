using System.Collections.Generic;

namespace GameStore.Models.Products
{
    public interface IProductContext
    {
        int AddProduct(Product product);

        void DeleteProduct(int id);

        IEnumerable<Product> GetAllProducts();

        int GetCountOfProducts();

        Product SelectProduct(int id);

        void UpdateProduct(Product product);
    }
}