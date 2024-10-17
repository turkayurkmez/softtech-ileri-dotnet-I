using eshop.Catalog.Application.Contracts;
using eshop.Catalog.Domain;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.Catalog.Application.Features.Products.CreateNewProduct
{
    public class CreateProductRequestHandler(IProductRepository repository) : IRequestHandler<CreateProductRequest, CreateProductResponse>
    {

        public async Task<CreateProductResponse> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            var product = request.Adapt<Product>();
            await repository.CreateAsync(product);
            return new CreateProductResponse(product.Id);
        }
    }
}
