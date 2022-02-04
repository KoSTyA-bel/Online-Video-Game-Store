using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Services.Products
{
    /// <summary>
    /// Abstract service builder.
    /// </summary>
    public abstract class ProductServiceBuilder
    {
        /// <summary>
        /// Creates a new instance of the service.
        /// </summary>
        /// <param name="context">Products context.</param>
        /// <returns>Specific implementation of the service.</returns>
        public abstract IProductService Build(IProductContext context);
    }
}
