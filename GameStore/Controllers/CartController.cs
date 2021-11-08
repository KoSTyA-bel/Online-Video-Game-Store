using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using GameStore.Models;
using System.Text.Json;
using GameStore.Models.Cart;

namespace GameStore.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private IProductService _productService;

        public CartController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(HttpContext.Session.Get<Cart>("Cart") ?? new Cart());
        }

        [HttpPost]
        public IActionResult AddProductToCart(int id, string returnUrl)
        {
            var cart = HttpContext.Session.Get<Cart>("Cart") ?? new Cart();

            if (_productService.TryShowProduct(id, out Product product))
            {
                var cartline = cart.CartLines.Where(x => x.Product.Equals(product)).FirstOrDefault();
                if (cartline is null)
                {
                    cart.CartLines.Add(new CartLine() { Product = product, Quantity = 1 });
                }
                else
                {
                    cart.CartLines.Remove(cartline);

                    cartline.Quantity++;
                    cart.CartLines.Add(cartline);
                }
            }

            HttpContext.Session.Set<Cart>("Cart", cart);

            return Redirect(returnUrl);
        }

        [HttpPost]
        public IActionResult RemoveProductFromCart(int id, string returnUrl)
        {
            var cart = HttpContext.Session.Get<Cart>("Cart") ?? new Cart();

            if (_productService.TryShowProduct(id, out Product product))
            {
                var cartline = cart.CartLines.Where(x => x.Product.Equals(product)).FirstOrDefault();
                if (cartline != null)
                {
                    cart.CartLines.Remove(cartline);
                }
            }

            HttpContext.Session.Set<Cart>("Cart", cart);

            return Redirect(returnUrl);
        }
    }
}
