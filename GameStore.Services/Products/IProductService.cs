using System.Collections.Generic;

namespace GameStore.Services.Products
{
    public interface IProductService
    {
        /// <summary>
        /// Adds a product to the database.
        /// </summary>
        /// <param name="product">The product to be added to the database.</param>
        /// <returns>True - if the product was successfully added, false - in all other cases.</returns>
        bool AddProduct(Product product);

        /// <summary>
        /// Creates a product, then adds it to the database.
        /// </summary>
        /// <param name="name">Name of product.</param>
        /// <param name="description">Description of the product.</param>
        /// <param name="price">Price of the product.</param>
        /// <param name="pathToPicture">Path to product picture.</param>
        /// <returns>True - if it was possible to create a product and add it to the database, false - in all other cases.</returns>
        bool CreateProduct(string name, string description, decimal price, string pathToPicture);

        /// <summary>
        /// Provides the entire set of products.
        /// </summary>
        /// <returns>Enumerated set of products.</returns>
        IEnumerable<Product> GetAllProducts();

        /// <summary>
        /// Allows you to delete a product from the database.
        /// </summary>
        /// <param name="product">Product to remove.</param>
        /// <returns>True - if you were able to delete the product, false - in all other cases.</returns>
        bool RemoveProduct(Product product);

        /// <summary>
        /// Allows you to see if there is a product with the Id passed to the method.
        /// </summary>
        /// <param name="id">Id of the product to be found.</param>
        /// <param name="product">Product search result.</param>
        /// <returns>True - if the product with the passed id exists, false - in all other cases.</returns>
        bool TryShowProduct(int id, out Product product);

        /// <summary>
        /// Updates product information in the database.
        /// </summary>
        /// <param name="product">The product whose information needs to be updated.</param>
        /// <returns>True - if it was possible to update the product information, false - in all other cases.</returns>
        bool UpdateProduct(Product product);

        /// <summary>
        /// Retrieves information about the latest product in the database.
        /// </summary>
        /// <returns>Product.</returns>
        Product GetLastProduct();
    }
}