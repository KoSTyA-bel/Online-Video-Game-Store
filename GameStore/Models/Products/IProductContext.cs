using System.Collections.Generic;

namespace GameStore.Models.Products
{
    /// <summary>
    /// It represents methods that must be implemented in classes that represent working with products database.
    /// </summary>
    public interface IProductContext
    {
        /// <summary>
        /// Adds product to the DB.
        /// </summary>
        /// <param name="product">Product.</param>
        /// <returns>Id of added product.</returns>
        int AddProduct(Product product);

        /// <summary>
        /// Remove product from DB.
        /// </summary>
        /// <param name="id">Product ID to delete.</param>
        void DeleteProduct(int id);

        /// <summary>
        /// Select all product that contains in DB.
        /// </summary>
        /// <returns>List of all products.</returns>
        IEnumerable<Product> GetAllProducts();

        /// <summary>
        /// Select count of products.
        /// </summary>
        /// <returns>Count of products.</returns>
        int GetCountOfProducts();

        /// <summary>
        /// Selects a specific product from the database.
        /// </summary>
        /// <param name="id">Product id to select from the DB.</param>
        /// <returns>Product.</returns>
        Product SelectProduct(int id);

        /// <summary>
        /// Updates product data.
        /// </summary>
        /// <param name="product">The product whose data needs to be updated.</param>
        void UpdateProduct(Product product);
    }
}