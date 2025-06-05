using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
using Shared.DataTransferObjects.ProductModuleDtos;

namespace ServicesAbstraction
{
    public interface IProductService
    {
        // Get All Products

        Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams);

        // Get Product By Id

        Task<ProductDto> GetProductByIdAsync(int id);

        //Get All Brands
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();

        // Get All Types 
        Task<IEnumerable<TypeDto>> GetAllTypesAsync();
    }
}
