using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace GameStore.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ну указано название продукта.")]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Ну указано описание продукта.")]
        public string Discription { get; set; }

        [Required(ErrorMessage = "Ну указана стоимость продукта.")]
        public decimal Price { get; set; }

        public IFormFile Picture { get; set; }
    }
}
