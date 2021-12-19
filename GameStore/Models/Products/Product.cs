using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Models
{
    /// <summary>
    /// Class-container of product information.
    /// </summary>
    public class Product: IEquatable<Product>
    {
        /// <summary>
        /// Crate a new instanse of <see cref="Product"/>.
        /// </summary>
        public Product()
        {
            Name = string.Empty;
            Description = string.Empty;
            PathToPicture = string.Empty;
        }

        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Product name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Product description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Product price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Path to product picture.
        /// </summary>
        public string PathToPicture { get; set; }

        public static bool operator ==(Product left, Product right) => left != null ? left.Equals(right) : right == null;

        public static bool operator !=(Product left, Product right) => !(left == right);

        public override int GetHashCode()
        {
            var res = this.Id;
            res <<= 5;
            res ^= Name.Length;
            res <<= 7;
            return res ^= Description.Length;
        }

        public override bool Equals(object obj)
        {
            try
            {
                return this.Equals((Product)obj);
            }
            catch
            {
                return false;
            }
        }

        public bool Equals(Product other)
        {
            if (other is null)
            {
                return false;
            }

            if (this.GetHashCode() != other.GetHashCode())
            {
                return false;
            }

            return other.Id == this.Id && string.Equals(other.Description, this.Description) && string.Equals(other.Name, this.Name) && other.Price == this.Price && string.Equals(other.PathToPicture, this.PathToPicture);
        }

        public override string ToString() 
        {
            return $"Product {Id}\n{Name}\n{Description}\n{Price}\n{PathToPicture}";
        }
    }
}
