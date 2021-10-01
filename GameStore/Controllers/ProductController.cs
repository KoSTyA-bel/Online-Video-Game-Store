using GameStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Controllers
{
    public class ProductController : Controller
    {
        private const long BYTES_OF_10_MB = 10485760;
        private IProductService _service;
        private IWebHostEnvironment _appEnvironment;

        public ProductController(IWebHostEnvironment appEnvironment, IProductService service)
        {
            _appEnvironment = appEnvironment ?? throw new ArgumentNullException(nameof(appEnvironment));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
        
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Products = _service.GetAllProducts();

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
            var product = new Product() { Name = model.Name };

            if (model.Picture is null)
            {
                ModelState.AddModelError("", "Не выбрана картинка продукта.");
            }
            else if (model.Picture.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("", "Выбран некорректный тип данных.");
            }
            else if (model.Picture.Length > BYTES_OF_10_MB)
            {
                ModelState.AddModelError("", "Файл весит слишком много.");
            }
            else
            {
                string picture = "images/" + model.Picture.FileName;
                string path =  Path.Combine(_appEnvironment.WebRootPath, picture);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await model.Picture.CopyToAsync(fileStream);

                    if (!_service.CreateProduct(model.Name, model.Discription, model.Price, picture))
                    {
                        ModelState.AddModelError("", "Невозможно создать новыйпродукт.");
                        return View();
                    }
                }
            }

            return Redirect("/Product");
        }
    }
}
