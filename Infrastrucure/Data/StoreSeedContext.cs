using Core.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastrucure.Data
{
    public class StoreSeedContext
    {
        public static async Task SeedAsync(StoreContext context) 
        { 
            if (!context.ProductBrands.Any())
            {
                var brandData = File.ReadAllText("../Infrastrucure/Data/SeedData/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                context.ProductBrands.AddRange(brands);
            }
            if (!context.ProductTypes.Any())
            {
                var typeData = File.ReadAllText("../Infrastrucure/Data/SeedData/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typeData);
                context.ProductTypes.AddRange(types);
            }
            if (!context.Proucts.Any())
            {
                var productData = File.ReadAllText("../Infrastrucure/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productData);
                context.Proucts.AddRange(products);
            }
            if (context.ChangeTracker.HasChanges())
                await context.SaveChangesAsync();
        }
    }
}
