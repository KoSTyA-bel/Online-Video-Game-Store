using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace GameStore.Models
{
    /// <summary>
    /// The model representing the product.
    /// </summary>
    public class ProductModel
    {
        /// <summary>
        /// Unique product identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Product name.
        /// </summary>
        [Required(ErrorMessage = "Ну указано название продукта.")]
        public string Name { get; set; }

        /// <summary>
        /// Product description.
        /// </summary>
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Ну указано описание продукта.")]
        public string Description { get; set; }

        /// <summary>
        /// Product price.
        /// </summary>
        [Required(ErrorMessage = "Ну указана стоимость продукта.")]
        public decimal Price { get; set; }

        /// <summary>
        /// Product picture.
        /// </summary>
        public IFormFile Picture { get; set; }
    }
}
