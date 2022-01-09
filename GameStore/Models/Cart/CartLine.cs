using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
