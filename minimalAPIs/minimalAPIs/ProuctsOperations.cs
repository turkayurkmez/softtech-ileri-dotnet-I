using Microsoft.EntityFrameworkCore;

namespace minimalAPIs
{
    public class ProuctsOperations
    {
        public async Task<IResult> GetAllProductsAsync(ProductsDb productsDb)
        {
            return TypedResults.Ok(await productsDb.Products.ToListAsync());
        }

        public async Task<IResult> GetProductById(int id, ProductsDb db)
        {

            var product = await db.Products.FindAsync(id);
            return TypedResults.Ok(product);

        }

        public async Task<IResult> SearchByName(string name, ProductsDb db)
        {

            var products = await db.Products.Where(p => p.Name.Contains(name)).ToListAsync();
            return TypedResults.Ok(products);
        }

        public async Task<IResult> CreateProduct(CreateProductRequest productDto, ProductsDb db)
        {
            var product = new Product { Name = productDto.Name, Price = productDto.Price };
            db.Products.Add(product);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/products/{product.Id}", product);
        }

        public async Task<IResult> UpdateExisting(int id, Product product, ProductsDb db)
        {
            var existingProduct = await db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            //var existingProduct = await db.Products.FindAsync(id);


            if (existingProduct is null)
            {
                return TypedResults.NotFound();
            }
            db.Products.Update(product);
            //existingProduct.Name = product.Name;
            //existingProduct.Price = product.Price;
            await db.SaveChangesAsync();
            return TypedResults.NoContent();
        }
    }
}
