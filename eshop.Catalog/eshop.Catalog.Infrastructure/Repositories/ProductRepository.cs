using eshop.Catalog.Application.Contracts;
using eshop.Catalog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
