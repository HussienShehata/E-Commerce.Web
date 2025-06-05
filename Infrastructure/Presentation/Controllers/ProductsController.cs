using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared;
using Shared.DataTransferObjects.ProductModuleDtos;

namespace Presentation.Controllers
{

    [ApiController]
    [Route(template: "api/[Controller]")] // BaseUrl/api/Products
    public class ProductsController(IServiceManager _serviceManager) : ControllerBase
    {
        // Get All Products
        [HttpGet]
        // GET BaseUrl/api/Products
        public async Task<ActionResult<PaginatedResult<ProductDto>>> GetAllProducts([FromQuery]ProductQueryParams queryParams)
        {
            var Products = await _serviceManager.ProductService.GetAllProductsAsync(queryParams);
            return Ok(value: Products);
        }

        // Get Product By ID 
        [HttpGet(template: "{id:int}")]
        // GET BaseUrl/api/Products/id
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var Product = await _serviceManager.ProductService.GetProductByIdAsync(id);
            return Ok(value: Product);
        }

        // Get All Types
        [HttpGet(template:"types")]
        // GET BaseUrl/api/Products/types
        public async Task<ActionResult<TypeDto>> GetAllTypes()
        {
            var Types = await _serviceManager.ProductService.GetAllTypesAsync();
            return Ok(value: Types);
        }

        // Get All Brands
        [HttpGet(template:"brands")]
        // GET BaseUrl/api/Products/brands
        public async Task<ActionResult<BrandDto>> GetAllBrands() 
        { 
            var Brands = await _serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(value: Brands);
        }
    } 
}
