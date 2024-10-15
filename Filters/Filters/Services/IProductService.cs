using Filters.Models;

namespace Filters.Services
{
    public interface IProductService
    {
        Product? GetProduct(int id);

        bool IsExists(int id);
    }
}