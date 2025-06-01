using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Shared;

namespace Services.Specifications
{
    internal class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithBrandAndTypeSpecifications(int? BrandId,int? TypeId, ProductSortingOptions sortingOptions) 
            : base(CriteriaExpression: P=> (!BrandId.HasValue || P.BrandId == BrandId) && (!TypeId.HasValue || P.TypeId== TypeId) )
        {
            AddInclude(includeExpression: P => P.ProductBrand);
            AddInclude(includeExpression: P => P.ProductType);

            switch (sortingOptions)
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
        }
        public ProductWithBrandAndTypeSpecifications(int id) : base(CriteriaExpression: P => P.Id == id)
        {
   
             AddInclude(includeExpression: P => P.ProductBrand);
             AddInclude(includeExpression: P => P.ProductType);
            
        }
    }
}
