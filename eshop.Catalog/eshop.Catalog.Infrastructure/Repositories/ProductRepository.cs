using eshop.Catalog.Application.Contracts;
using eshop.Catalog.Domain;
using eshop.Catalog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogDbContext dbContext;

        public ProductRepository(CatalogDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(Product entity)
        {
            dbContext.Products.Add(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await dbContext.Products.ToListAsync();
        }
    }
}
