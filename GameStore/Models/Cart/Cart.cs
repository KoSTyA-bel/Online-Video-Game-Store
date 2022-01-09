    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Models.Cart
{
    public class Cart
    {
        /// <summary>
        /// Crate a new instanse of <see cref="Cart"/>.
        /// </summary>
        public Cart()
        {
            CartLines = new();
        }

        /// <summary>
        /// Crate a new instanse of <see cref="Cart"/>.
        /// </summary>
        /// <param name="cartLines">List of cart lines.</param>
        public Cart(IEnumerable<CartLine> cartLines)
        {
            CartLines = cartLines.ToList() ?? throw new ArgumentNullException(nameof(cartLines));
        }

        /// <summary>
        /// List of cart lines.
        /// </summary>
        public List<CartLine> CartLines { get; set; }

        /// <summary>
        /// Adds product into the cart.
        /// </summary>
        /// <param name="product">Product that must be added to the cart.</param>
        /// <returns>True if product successfully added, in other cases false.</returns>
        public bool AddProductToCart(Product product)
        {
            if (product is null)
            {
                return false;
            }

            var cartline = this.CartLines.Where(x => x.Product.Equals(product)).FirstOrDefault();

            if (cartline is null)
            {
                this.CartLines.Add(new CartLine() { Product = product, Quantity = 1 });
            }
            else
            {
                this.CartLines.Remove(cartline);

                cartline.Quantity++;
                this.CartLines.Add(cartline);
            }

            return true;
        }

        /// <summary>
        /// Remove product from cart.
        /// </summary>
        /// <param name="product">Product that must be removed from cart.</param>
        /// <returns>if product successfully removed true, false in all other cases.</returns>
        public bool RemoveProductFromCart(Product product)
        {
            if (product is null)
            {
                return false;
            }

            var cartline = this.CartLines.Where(x => x.Product.Equals(product)).FirstOrDefault();

            if (cartline != null)
            {
                this.CartLines.Remove(cartline);
                return true;
            }

            return false;
        }
    }
}
