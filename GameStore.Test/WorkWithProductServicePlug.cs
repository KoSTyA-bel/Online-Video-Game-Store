using System;
using GameStore.Models;

namespace GameStore.Test
{
    class WorkWithProductServicePlug
    {
        private IProductService _service;

        public WorkWithProductServicePlug(IProductService service)
        {
            _service = service;
        }

        public void Create()
        {
            _service.AddProduct(null);
        }

        public void Remove()
        {
            _service.RemoveProduct(null);
        }

        public void Update()
        {
            _service.UpdateProduct(null);
        }
    }
}
