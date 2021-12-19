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
        public IActionResult AddProductToCart(int id)
        {
            var cart = HttpContext.Session.Get<Cart>("Cart") ?? new Cart();

            if (_productService.TryShowProduct(id, out Product product))
            {
                cart.AddProductToCart(product);
            }

            HttpContext.Session.Set<Cart>("Cart", cart);

            return Accepted((object)"Продукт успешно добавлен");
        }

        [HttpPost]
        public IActionResult Buy()
        {
            var cart = new Cart();

            HttpContext.Session.Set<Cart>("Cart", cart);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveProductFromCart(int id)
        {
            var cart = HttpContext.Session.Get<Cart>("Cart") ?? new Cart();

            if (_productService.TryShowProduct(id, out Product product))
            {
                cart.RemoveProductFromCart(product);
            }

            HttpContext.Session.Set<Cart>("Cart", cart);

            return RedirectToAction("Index");
        }
    }
}
