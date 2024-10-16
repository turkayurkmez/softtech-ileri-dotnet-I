using Filters.Models;
using System.Diagnostics;

namespace Filters.Services
{
    public class ProductService : IProductService
    {
        private ILogger<ProductService> _logger;
        private Stopwatch stopwatch;

        public ProductService(ILogger<ProductService> logger)
        {
            _logger = logger;
        }

        private List<Product> products = new()
        {
             new(){ Id=1, Name="A", Price=1},
             new(){ Id=2, Name="B", Price=1},
             new(){ Id=3, Name="C", Price=1}

        };

        public Product? GetProduct(int id)
        {
            stopwatch = Stopwatch.StartNew();
            var product = products.Find(p => p.Id == id);
            if (stopwatch.IsRunning)
            {
                stopwatch.Stop();
                _logger.LogInformation($"db'de geçen toplam süresi: {stopwatch.Elapsed.TotalMilliseconds}");
            }

            return product; 
        }

        public bool IsExists(int id)
        {
            return products.Any(p => p.Id == id);
        }
    }
}
