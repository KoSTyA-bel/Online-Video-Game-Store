using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Models
{
    public class ProductService : IProductService
    {
        private ProductContext _context;

        public ProductService(ProductContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public bool AddProduct(Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }


            if (_context.Products.Contains(product))
            {
                return false;
            }

            _context.Add(product);

            _context.SaveChanges();


            return true;
        }

        public IEnumerable<Product> GetAllProducts() => _context.Products;

        public bool RemoveProduct(Product product)
        {

            if (_context.Products.Contains(product))
            {
                _context.Remove(product);
                _context.SaveChanges();

                return true;
            }


            return false;
        }

        public bool UpdateProduct(Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
                //return false;
            }


            var productToUpdate = _context.Products.Where(x => x.Id == product.Id).FirstOrDefault();

            if (productToUpdate is null)
            {
                throw new ArgumentNullException(nameof(product));
                //return false;
            }

            productToUpdate.Name = product.Name;
            productToUpdate.Description = product.Description;
            productToUpdate.Price = product.Price;

            _context.SaveChanges();


            return true;
        }

        public bool TryShowProduct(int id, out Product product)
        {
            product = _context.Products.Where(x => x.Id == id).FirstOrDefault();

            return product is null;
        }
    }
}
