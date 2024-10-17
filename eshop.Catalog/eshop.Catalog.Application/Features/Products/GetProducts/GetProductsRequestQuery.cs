using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.Catalog.Application.Features.Products.GetProducts
{
    //NŞA'da Query kelimesiyle bitmesi gerekmez!
    public record GetProductsRequestQuery() : IRequest<GetProductsResponse>;


   

}
