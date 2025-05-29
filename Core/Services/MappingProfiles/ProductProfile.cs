using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Models;
using Shared.DataTransferObjects;

namespace Services.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product,ProductDto>()
                .ForMember(destinationMember: P=>P.BrandName , memberOptions: Options=>Options.MapFrom(mapExpression: Src=>Src.ProductBrand))
                .ForMember(destinationMember: P=>P.BrandType,memberOptions: Options=>Options.MapFrom(mapExpression: Src=>Src.ProductType))
                .ForMember(destinationMember: P=>P.PictureUrl, memberOptions: Options=>Options.MapFrom<PictureUrlResolver>());

            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductType, TypeDto>();
        }
    }
}
