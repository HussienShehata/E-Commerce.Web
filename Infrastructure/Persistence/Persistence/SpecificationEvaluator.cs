using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;


namespace Services
{
     static class SpecificationEvaluator
    {
        // Create Query

        public static IQueryable<TEntity> CreateQuery<TEntity, Tkey>(IQueryable<TEntity> InputQuery, ISpecifications<TEntity, Tkey> specifications) where TEntity : BaseEntity<Tkey>
        {
            var Query = InputQuery;
            if (specifications.Criteria is not null)
            {
                Query = Query.Where(specifications.Criteria);
            }
            if (specifications.OrderBy is not null)
            {
                Query= Query.OrderBy(specifications.OrderBy);
            }
            if(specifications.OrderByDesc is not null)
            {
                Query=Query.OrderByDescending(specifications.OrderByDesc);
            }
            if (specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Count > 0)
            {
                //foreach (var exp in specifications.IncludeExpressions)
                //{
                //    Query = Query.Include(navigationPropertyPath: exp);
                //}

                Query= specifications.IncludeExpressions.Aggregate(seed:Query , func:(CurrentQuery,IncludeExp)=> CurrentQuery.Include(IncludeExp)); // second overload of "Aggregate()"
            }
            return Query;
        }

    

     }
}
