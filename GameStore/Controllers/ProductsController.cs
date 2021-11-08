using GameStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace GameStore.Controllers
{
    public class ProductsController : Controller
    {
        private const long BYTES_OF_10_MB = 10485760;
        private IProductService _service;
        private IWebHostEnvironment _appEnvironment;

        public ProductsController(IWebHostEnvironment appEnvironment, IProductService service)
        {
            _appEnvironment = appEnvironment ?? throw new ArgumentNullException(nameof(appEnvironment));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        public ActionResult Index(int? id)
        {
            if (id is null)
            {
                return View();
            }

            if (_service.TryShowProduct((int)id, out Product product))
            {
                return View("Product", product);
            }

            return Redirect("/Products");
        }

        [HttpPost]
        public ActionResult Search(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return PartialView(_service.GetAllProducts());
            }

            name = name.ToUpper();

            return PartialView(_service.GetAllProducts().Where(product => product.Name.ToUpper().Contains(name)));
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
                string path = Path.Combine(_appEnvironment.WebRootPath, picture);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await model.Picture.CopyToAsync(fileStream);

                    if (!_service.CreateProduct(model.Name, model.Description, model.Price, picture))
                    {
                        ModelState.AddModelError("", "Невозможно создать новыйпродукт.");
                        return View();
                    }
                }
            }

            return Redirect($"/Product/{_service.GetAllProducts().Last().Id}");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("Update/{id}")]
        public ActionResult Update(int id)
        {
            _service.TryShowProduct(id, out Product product);
            return View(product);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Product model)
        {
            _service.UpdateProduct(model);

            return Redirect($"/Product/{_service.GetAllProducts().Last().Id}");
        }
    }
}
