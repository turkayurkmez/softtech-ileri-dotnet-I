using Microsoft.EntityFrameworkCore;

namespace minimalAPIs
{
    public class ProductsDb : DbContext
    {
        public ProductsDb(DbContextOptions<ProductsDb> options): base(options) 
        {
            
        }

        public DbSet<Product> Products { get; set; }
    }
}
