using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.Catalog.Application.Features.Products.CreateNewProduct
{

    public record CreateProductResponse(int Id); 
    public record CreateProductRequest(string Name, string? Description, decimal? Price, string?ImageUrl, int? CategoryId) : IRequest<CreateProductResponse>;
  
}
