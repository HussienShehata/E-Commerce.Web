using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;

namespace DomainLayer.Contracts
{
    public interface ISpecifications<TEntity,Tkey> where TEntity : BaseEntity<Tkey>
    {
        public Expression<Func<TEntity, bool>> Criteria { get; }
        public List<Expression<Func<TEntity,object>>> IncludeExpressions { get; }
    }
}
