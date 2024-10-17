using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.Catalog.Application.Features.Products.GetProducts
{
    public record ProductDisplayInfo(int Id, string Name, string Description, decimal? Price, string? ImageUrl);
    public record GetProductsResponse(IEnumerable<ProductDisplayInfo> Products);
    

}
