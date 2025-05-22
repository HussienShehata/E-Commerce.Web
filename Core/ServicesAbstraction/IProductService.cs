using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects;

namespace ServicesAbstraction
{
    public interface IProductService
    {
        // Get All Products

        Task<IEnumerable<ProductDto>> GetAllProductsAsync();

        // Get Product By Id

        Task<ProductDto> GetProductByIdAsync(int id);

        //Get All Brands
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();

        // Get All Types 
        Task<IEnumerable<TypeDto>> GetAllTypesAsync();
    }
}
