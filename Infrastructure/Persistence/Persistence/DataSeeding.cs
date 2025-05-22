using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence
{
    public class DataSeeding(StoreDbContext _dbContext) : IDataSeeding
    {
        public async Task DataSeedAsync()
        {
            try
            {
                var PendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
                if (PendingMigrations.Any())
                   await _dbContext.Database.MigrateAsync();

                if (!_dbContext.ProductBrands.Any())
                {
                   // var ProductBrandData = File.ReadAllText(@"..\Infrastructure\Persistence\Persistence\Data\DataSeed\brands.json");
                   var ProductBrandData = File.OpenRead(@"..\Infrastructure\Persistence\Persistence\Data\DataSeed\brands.json");
                    
                    // Convert data "string" to C# Object [ProductBrands]
                    var ProductBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(utf8Json: ProductBrandData);
                    if (ProductBrands != null && ProductBrands.Any())
                       await _dbContext.ProductBrands.AddRangeAsync(entities: ProductBrands);

                }

                if (!_dbContext.ProductTypes.Any())
                {
                  //  var ProductTypeData = File.ReadAllText(@"..\Infrastructure\Persistence\Persistence\Data\DataSeed\types.json");
                    var ProductTypeData = File.OpenRead(@"..\Infrastructure\Persistence\Persistence\Data\DataSeed\types.json");

                    // Convert data "string" to C# Object [ProductBrands]
                    var ProductTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>( utf8Json: ProductTypeData);
                    if (ProductTypes != null && ProductTypes.Any())
                       await _dbContext.ProductTypes.AddRangeAsync(entities: ProductTypes);

                }

                if (!_dbContext.Products.Any())
                {
                    //var ProductData = File.ReadAllText(@"..\Infrastructure\Persistence\Persistence\Data\DataSeed\products.json");
                    var ProductData = File.OpenRead(@"..\Infrastructure\Persistence\Persistence\Data\DataSeed\products.json");
                    // Convert data "string" to C# Object [Products]
                    var Products = await JsonSerializer.DeserializeAsync<List<Product>>(utf8Json: ProductData);
                    if (Products != null && Products.Any())
                       await _dbContext.Products.AddRangeAsync(entities: Products);

                }


               await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                // To Do;
            }
        }
    }
}
