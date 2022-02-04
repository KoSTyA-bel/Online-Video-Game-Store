using GameStore.Services.Products;

namespace GameStore.Models.Cart
{
    /// <summary>
    /// Container class that stores information about the number of products to purchase and the product itself.
    /// </summary>
    public class CartLine
    {
        /// <summary>
        /// Product.
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// Number of products.
        /// </summary>
        public int Quantity { get; set; }
    }
}
