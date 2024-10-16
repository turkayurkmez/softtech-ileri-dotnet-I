using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.Catalog.Application.Services
{
    public record ProductDisplayResponse(int Id, string Name, decimal? Price, string? ImageUrl);
    public record CreateProductRequest(string Name, decimal? Price, string? ImageUrl);

    public interface IProductService
    {
        Task<IEnumerable<ProductDisplayResponse>> GetProductsAsync();
        Task CreateNewProduct(CreateProductRequest request);

        //Uygulamanız ürün nesnesi ile her çalışmak istediğinde (her yeni iş için o işin karşılığı olarak), buraya fonksiyon yazmalısınız!


    }
}
