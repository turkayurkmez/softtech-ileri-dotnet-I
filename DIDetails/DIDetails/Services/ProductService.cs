using DIDetails.Models;

namespace DIDetails.Services
{
    public class ProductService : IProductService
    {
        public List<Product> GetProducts()
        {

            return new List<Product>() {
                new() {  Id=1, Name="Ürün 1"},
                new() {  Id=2, Name="Ürün 2"},
                new() {  Id=3, Name="Ürün 3"},

            };
        }

    }
}
