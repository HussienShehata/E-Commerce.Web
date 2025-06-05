using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Models.ProductModule;
using Shared.DataTransferObjects.ProductModuleDtos;

namespace Services.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product,ProductDto>()
                .ForMember(destinationMember: P=>P.BrandName , memberOptions: Options=>Options.MapFrom(mapExpression: Src=>Src.ProductBrand.Name))
                .ForMember(destinationMember: P=>P.BrandType,memberOptions: Options=>Options.MapFrom(mapExpression: Src=>Src.ProductType.Name))
                .ForMember(destinationMember: P=>P.PictureUrl, memberOptions: Options=>Options.MapFrom<PictureUrlResolver>());

            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductType, TypeDto>();
        }
    }
}
