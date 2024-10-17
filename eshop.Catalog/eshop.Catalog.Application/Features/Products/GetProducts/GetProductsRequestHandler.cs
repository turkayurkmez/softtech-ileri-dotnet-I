using eshop.Catalog.Application.Contracts;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.Catalog.Application.Features.Products.GetProducts
{
    public class GetProductsRequestHandler : IRequestHandler<GetProductsRequestQuery, GetProductsResponse>
    {
        private readonly IProductRepository productRepository;

        public GetProductsRequestHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        //request'i al -> response'u gönder!
        public async Task<GetProductsResponse> Handle(GetProductsRequestQuery request, CancellationToken cancellationToken)
        {
            var products = await productRepository.GetAllAsync();

            var displayInfos = products.Adapt<IEnumerable<ProductDisplayInfo>>();
            var response = new GetProductsResponse(displayInfos);
            return response;
        }
    }
}
