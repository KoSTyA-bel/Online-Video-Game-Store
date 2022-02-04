using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Http;
using GameStore.Models;
using GameStore.Models.Cart;
using GameStore.Services.Products;

namespace GameStore.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IProductService _productService;

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
