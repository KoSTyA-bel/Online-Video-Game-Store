using System;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Services.Products
{
    /// <summary>
    /// Class-container of product information.
    /// </summary>
    public class Product : IEquatable<Product>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        public Product()
        {
            Name = string.Empty;
            Description = string.Empty;
            PathToPicture = string.Empty;
        }

        /// <summary>
        /// Gets or sets product id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets product name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets product description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets product price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets path to product picture.
        /// </summary>
        public string PathToPicture { get; set; }

        public static bool operator ==(Product left, Product right) => left != null ? left.Equals(right) : right == null;

        public static bool operator !=(Product left, Product right) => !(left == right);

        /// <summary>
        /// Deconstructs an object.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:Element parameters should be documented", Justification = "<Ожидание>")]
        public void Deconstruct(out int id, out string name, out string desctipriptin, out decimal price, out string pathToPicture)
        {
            id = this.Id;
            name = this.Name;
            desctipriptin = this.Description;
            price = this.Price;
            pathToPicture = this.PathToPicture;
        }

        /// <summary>
        /// Deconstructs an object.
        /// </summary>
        /// <returns>Values.</returns>
        public (int id, string name, string description, decimal price, string pathToPicture) Deconstruct() => (this.Id, this.Name, this.Description, this.Price, this.PathToPicture);

        public override int GetHashCode()
        {
            var res = this.Id;
            res <<= 5;
            res ^= Name.Length;
            res <<= 7;
            return res ^= Description.Length;
        }

        public override bool Equals(object obj) => obj is Product product ? Equals(product) : false;

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

            return other.Id == this.Id 
                && string.Equals(other.Description, this.Description) 
                && string.Equals(other.Name, this.Name) 
                && other.Price == this.Price 
                && string.Equals(other.PathToPicture, this.PathToPicture);
        }

        public override string ToString() 
        {
            return $"Product {Id}\n{Name}\n{Description}\n{Price}\n{PathToPicture}";
        }
    }
}
