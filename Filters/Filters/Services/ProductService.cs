using Filters.Models;

namespace Filters.Services
{
    public class ProductService : IProductService
    {
        private List<Product> products = new()
        {
             new(){ Id=1, Name="A", Price=1},
             new(){ Id=2, Name="B", Price=1},
             new(){ Id=3, Name="C", Price=1}

        };

        public Product? GetProduct(int id)
        {
            return products.Find(p => p.Id == id);
        }

        public bool IsExists(int id)
        {
            return products.Any(p => p.Id == id);
        }
    }
}
