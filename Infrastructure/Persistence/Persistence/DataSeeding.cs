using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using DomainLayer.Models.ProductModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Identity;

namespace Persistence
{
    public class DataSeeding(StoreDbContext _dbContext, 
        UserManager<ApplicationUser> _userManager,
        RoleManager<IdentityRole> _roleManager,
        StoreIdentityDbContext _identityDbContext) : IDataSeeding
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

        public async Task IdentityDataSeedAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(role: new IdentityRole(roleName: "Admin"));
                    await _roleManager.CreateAsync(role: new IdentityRole(roleName: "SuperAdmin"));
                }

                if (!_userManager.Users.Any())
                {
                    var User01 = new ApplicationUser()
                    {
                        Email = "Mohamed@gmail.com",
                        DisplayName = "Mohamed Tarek",
                        PhoneNumber = "0123456789",
                        UserName = "MohamedTarek"
                    };
                    var User02 = new ApplicationUser()
                    {
                        Email = "Salama@gmail.com",
                        DisplayName = "Salma Mohamed",
                        PhoneNumber = "0123456789",
                        UserName = "SalmaMohamed"
                    };
                    await _userManager.CreateAsync(user: User01, password: "P@ss0rd");
                    await _userManager.CreateAsync(user: User02, password: "P@ssw0rd");
                }
                await _identityDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
