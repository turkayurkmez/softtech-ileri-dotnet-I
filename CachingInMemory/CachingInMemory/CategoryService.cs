using Microsoft.Extensions.Caching.Memory;

namespace CachingInMemory
{
    public record Category(int Id, string Name);
    public class CategoryService(ILogger<CategoryService> logger, IMemoryCache memoryCache)
    {

        public async Task<IEnumerable<Category>> GetCategories()
        {

            if (!memoryCache.TryGetValue(CacheKeys.Categories, out IEnumerable<Category>? categories))
            {
                logger.LogInformation("db'den kategoriler çekiliyor...");
                await Task.Delay(3000);
                categories = new List<Category> { new Category(1, "Elektronik"), new Category(2, "Giyim") };

                var entryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(1))
                                                                .RegisterPostEvictionCallback(callback);


                memoryCache.Set("Categories", categories, entryOptions);
            }
            else
            {
                logger.LogInformation($"cache'den kategoriler çekiliyor...{DateTime.Now}");
            }

        
            return categories;

        }

        private void callback(object key, object? value, EvictionReason reason, object? state)
        {
            logger.LogInformation($"{key} verisi cache'den kaldırıldı");
        }
    }
}
