
using Microsoft.Extensions.Caching.Memory;

namespace CachingInMemory
{
    public class CategoriesCacheBackgroundService(IServiceProvider serviceProvider, ILogger<CategoriesCacheBackgroundService> logger, IMemoryCache memoryCache) : BackgroundService
    {

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //Her 3 dakikada bir cache'i otomatik boşalt ve tekrar doldur!
            if (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("CACHE arka planda güncelleniyor....");
                using var scope = serviceProvider.CreateScope();
                var categoryService = scope.ServiceProvider.GetRequiredService<CategoryService>();
                var categories = await categoryService.GetCategories();
                memoryCache.Remove(CacheKeys.Categories);
                memoryCache.Set(CacheKeys.Categories, categories, TimeSpan.FromMinutes(3));

                await Task.Delay(TimeSpan.FromMinutes(3), stoppingToken);
            }
        }
    }
}
