using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameStore.Services.Products
{
    public class ProductContextADOThreading : ProductContextADO, IProductContext
    {
        private static readonly object Locker = new ();

        public ProductContextADOThreading(string connectionString)
            : base(connectionString)
        {
        }

        public override int AddProduct(Product product)
        {
            var task = Task<int>.Factory.StartNew(() => 
            { 
                lock (Locker)
                {
                    return base.AddProduct(product);
                }
            });

            return task.GetAwaiter().GetResult();
        }

        public override void DeleteProduct(int id)
        {
            Task.Factory.StartNew(() =>
            {
                lock (Locker)
                {
                    base.DeleteProduct(id);
                }
            });
        }

        public override IEnumerable<Product> GetAllProducts()
        {
            var task = Task<IEnumerable<Product>>.Factory.StartNew(() =>
            {
                lock (Locker)
                {
                    return base.GetAllProducts();
                }
            });

            return task.GetAwaiter().GetResult();
        }

        public override int GetCountOfProducts()
        {
            var task = Task<int>.Factory.StartNew(() => 
            {
                lock (Locker)
                {
                    return base.GetCountOfProducts();
                }
            });

            return task.GetAwaiter().GetResult();
        }

        public override Product SelectProduct(int id)
        {
            var task = Task<Product>.Factory.StartNew(() => 
            {
                lock (Locker)
                {
                    return base.SelectProduct(id);
                }
            });

            return task.GetAwaiter().GetResult();
        }

        public override void UpdateProduct(Product product)
        {
            Task.Factory.StartNew(() =>
            {
                lock (Locker)
                {
                    base.UpdateProduct(product);
                }
            });
        }
    }
}
