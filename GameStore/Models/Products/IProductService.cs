using System.Collections.Generic;
using System.IO;

namespace GameStore.Models
{
    public interface IProductService
    {
        bool AddProduct(Product product);

        bool CreateProduct(string name, string description, decimal price, string pathToPicture);

        IEnumerable<Product> GetAllProducts();

        bool RemoveProduct(Product product);

        bool TryShowProduct(int id, out Product product);

        bool UpdateProduct(Product product);
    }
}