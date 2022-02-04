using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Services.Products
{
    /// <summary>
    /// Concreate service builder
    /// </summary>
    public class ConcreateProductServiceBuilder : ProductServiceBuilder
    {
        /// <inheritdoc/>
        public override IProductService Build(IProductContext context) => new ProductService(context);
    }
}
