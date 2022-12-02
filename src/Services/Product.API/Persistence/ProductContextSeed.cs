using Product.API.Entities;
using Serilog;

namespace Product.API.Persistence
{
    public static class ProductContextSeed
    {
        public static async Task SeedProductAsync(ProductContext productContext, Serilog.ILogger logger)
        {
            if(!productContext.Products.Any())
            {
                productContext.AddRange(GetCatalogProducts());
                await productContext.SaveChangesAsync();
                logger.Information("Seed data for Product DB associated with context {DbContextName}", nameof(productContext));
            }
        }

        private static IEnumerable<CatalogProduct> GetCatalogProducts()
        {
            return new List<CatalogProduct>
            {
                new CatalogProduct
                {
                    No = "Iphone_14",
                    Name = "Iphone 14",
                    Summary = "Lorem ipsum, or lipsum as it is sometimes known, is dummy text used in laying out pr",
                    Description = "Lorem ipsum, or lipsum as it is sometimes known, is dummy text used in laying out pr",
                    Price = (decimal)500.06
                },
                new CatalogProduct
                {
                    No = "Iphone_8",
                    Name = "Iphone 8",
                    Summary = "Lorem ipsum, or lipsum as it is sometimes known, is dummy text used in laying out pr",
                    Description = "Lorem ipsum, or lipsum as it is sometimes known, is dummy text used in laying out pr",
                    Price = (decimal)100.06
                }
            };
        }
    }
}
