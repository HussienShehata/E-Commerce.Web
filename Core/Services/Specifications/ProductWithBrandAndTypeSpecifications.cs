using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Services.Specifications
{
    internal class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithBrandAndTypeSpecifications(int? BrandId,int? TypeId) 
            : base(CriteriaExpression: P=> (!BrandId.HasValue || P.BrandId == BrandId) && (!TypeId.HasValue || P.TypeId== TypeId) )
        {
            AddInclude(includeExpression: P => P.ProductBrand);
            AddInclude(includeExpression: P => P.ProductType);
        }
        public ProductWithBrandAndTypeSpecifications(int id) : base(CriteriaExpression: P => P.Id == id)
        {
   
             AddInclude(includeExpression: P => P.ProductBrand);
             AddInclude(includeExpression: P => P.ProductType);
            
        }
    }
}
