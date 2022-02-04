using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameStore.Services.Products
{
    public class ProductContextADOThreading : ProductContextADO, IProductContext
    {
        private object locker = new();

        public ProductContextADOThreading(string connectionString)
            : base(connectionString)
        {
        }

        public override int AddProduct(Product product)
        {
            var task = Task<int>.Factory.StartNew(() => 
            { 
                lock (locker)
                {
                    return base.AddProduct(product);
                }
            });

            task.Wait();

            return task.Result;
        }

        public override void DeleteProduct(int id)
        {
            Task.Factory.StartNew(() =>
            {
                lock (locker)
                {
                    base.DeleteProduct(id);
                }
            });
        }

        public override IEnumerable<Product> GetAllProducts()
        {
            var task = Task<IEnumerable<Product>>.Factory.StartNew(() =>
            {
                lock (locker)
                {
                    return base.GetAllProducts();
                }
            });

            task.Wait();

            return task.Result;
        }

        public override int GetCountOfProducts()
        {
            var task = Task<int>.Factory.StartNew(() => 
            {
                lock (locker)
                {
                    return base.GetCountOfProducts();
                }
            });

            task.Wait();

            return task.Result;
        }

        public override Product SelectProduct(int id)
        {
            var task = Task<Product>.Factory.StartNew(() => 
            {
                lock (locker)
                {
                    return base.SelectProduct(id);
                }
            });

            task.Wait();

            return task.Result;
        }

        public override void UpdateProduct(Product product)
        {
            Task.Factory.StartNew(() =>
            {
                lock (locker)
                {
                    base.UpdateProduct(product);
                }
            });
        }
    }
}
