using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.ProductModule;
using Services.Specifications;
using ServicesAbstraction;
using Shared;
using Shared.DataTransferObjects;

namespace Services
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var Repo = _unitOfWork.GetRepository<ProductBrand,int>();
            var Brands = await Repo.GetAllAsync();
            var BrandsDto = _mapper.Map<IEnumerable<ProductBrand>,IEnumerable<BrandDto>>(source: Brands);
            return BrandsDto;
         }

        public async Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            var Repo = _unitOfWork.GetRepository<Product, int>();
            var specifications = new ProductWithBrandAndTypeSpecifications(queryParams);
            var Products = await Repo.GetAllAsync(specifications);
            var Data =_mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(source: Products);
            var ProductCount = Products.Count();
            var CountSpec = new ProductCountSpecifications(queryParams);
            var TotalCount = await Repo.CountAsync(CountSpec);

            return new PaginatedResult<ProductDto>(pageIndex: queryParams.PageIndex,pageSize:ProductCount ,totalCount:TotalCount,data:Data );
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
           var Types = await _unitOfWork.GetRepository<ProductType,int>().GetAllAsync();
           var TypesDto = _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(source: Types);
           return TypesDto;
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var specifications = new ProductWithBrandAndTypeSpecifications(id);
            var Product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(specifications);
            if(Product is null)
            {
                throw new ProductNotFoundException(id);
            }
            var ProductDto = _mapper.Map<Product, ProductDto>(source: Product);
            return ProductDto;
        }
    }
}
