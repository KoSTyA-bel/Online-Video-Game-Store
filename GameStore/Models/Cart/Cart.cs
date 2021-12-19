    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Models.Cart
{
    public class Cart
    {
        public Cart()
        {
            CartLines = new();
        }

        public Cart(IEnumerable<CartLine> cartlines)
        {
            CartLines = cartlines.ToList() ?? throw new ArgumentNullException(nameof(cartlines));
        }

        public List<CartLine> CartLines { get; set; }

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
