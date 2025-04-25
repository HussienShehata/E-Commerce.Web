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
        public void DataSeed()
        {
            try
            {
                if (_dbContext.Database.GetPendingMigrations().Any())
                    _dbContext.Database.Migrate();

                if (!_dbContext.ProductBrands.Any())
                {
                    var ProductBrandData = File.ReadAllText(@"..\Infrastructure\Persistence\Persistence\Data\DataSeed\brands.json");
                    // Convert data "string" to C# Object [ProductBrands]
                    var ProductBrands = JsonSerializer.Deserialize<List<ProductBrand>>(json: ProductBrandData);
                    if (ProductBrandData != null && ProductBrandData.Any())
                        _dbContext.ProductBrands.AddRange(entities: ProductBrands);

                }

                if (!_dbContext.ProductTypes.Any())
                {
                    var ProductTypeData = File.ReadAllText(@"..\Infrastructure\Persistence\Persistence\Data\DataSeed\types.json");
                    // Convert data "string" to C# Object [ProductBrands]
                    var ProductTypes = JsonSerializer.Deserialize<List<ProductType>>(json: ProductTypeData);
                    if (ProductTypeData != null && ProductTypeData.Any())
                        _dbContext.ProductTypes.AddRange(entities: ProductTypes);

                }

                if (!_dbContext.Products.Any())
                {
                    var ProductData = File.ReadAllText(@"..\Infrastructure\Persistence\Persistence\Data\DataSeed\products.json");
                    // Convert data "string" to C# Object [Products]
                    var Products = JsonSerializer.Deserialize<List<Product>>(json: ProductData);
                    if (ProductData != null && ProductData.Any())
                        _dbContext.Products.AddRange(entities: Products);

                }


                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                // To Do;
            }
        }
    }
}
