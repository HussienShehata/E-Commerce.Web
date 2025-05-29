using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Services.Specifications;
using ServicesAbstraction;
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

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var specifications = new ProductWithBrandAndTypeSpecifications();
            var Products = await _unitOfWork.GetRepository<Product,int>().GetAllAsync(specifications);
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(source: Products);
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
            var ProductDto = _mapper.Map<Product, ProductDto>(source: Product);
            return ProductDto;
        }
    }
}
