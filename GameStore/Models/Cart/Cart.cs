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

        public Cart(List<CartLine> cartLines)
        {
            CartLines = cartLines ?? throw new ArgumentNullException(nameof(cartLines));
        }

        public List<CartLine> CartLines { get; set; }
    }
}
