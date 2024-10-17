using eshop.Catalog.Application.Contracts;
using eshop.Catalog.Application.Features.Products.GetProducts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace eshop.Catalog.Application.Extensions
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
         
           services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}
