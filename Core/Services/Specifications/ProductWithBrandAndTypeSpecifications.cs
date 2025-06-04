using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.ProductModule;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Shared;

namespace Services.Specifications
{
    internal class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithBrandAndTypeSpecifications(ProductQueryParams queryParams)
            : base(CriteriaExpression: P => (!queryParams.BrandId.HasValue || P.BrandId == queryParams.BrandId)
            && (!queryParams.TypeId.HasValue || P.TypeId == queryParams.TypeId)
            && (string.IsNullOrWhiteSpace(queryParams.SearchValue) || P.Name.ToLower().Contains(queryParams.SearchValue.ToLower())))
        {
            AddInclude(includeExpression: P => P.ProductBrand);
            AddInclude(includeExpression: P => P.ProductType);

            switch (queryParams.SortingOptions)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(orderByExp: P => P.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDesc(orderByDescExp: P => P.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(orderByExp: P => P.Price);
                     break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDesc(orderByDescExp: P => P.Price);
                    break;
                default:
                    break;


            }

            ApplyPagination(queryParams.PageSize, queryParams.PageIndex);
        }
        public ProductWithBrandAndTypeSpecifications(int id) : base(CriteriaExpression: P => P.Id == id)
        {
   
             AddInclude(includeExpression: P => P.ProductBrand);
             AddInclude(includeExpression: P => P.ProductType);
            
        }
    }
}
