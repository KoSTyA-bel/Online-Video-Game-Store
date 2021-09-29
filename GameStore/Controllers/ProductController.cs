using GameStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Controllers
{
    public class ProductController : Controller
    {
        // GET: ProductController
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.ProductModel model)
        {
            var product = new Product() { Name=model.Name };
            if (model.Picture is null)
            {
                ModelState.AddModelError("", "Не выбрана картинка продукта.");
            }
            else if (model.Picture.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("", "Выбран некорректный тип данных.");
            }

            return View();
        }
    }
}
