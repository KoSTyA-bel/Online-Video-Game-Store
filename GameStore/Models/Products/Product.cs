using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Models
{
    public class Product: IEquatable<Product>
    {
        public Product()
        {
            Name = string.Empty;
            Description = string.Empty;
            PathToPicture = string.Empty;
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string PathToPicture { get; set; }

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
    }
}
