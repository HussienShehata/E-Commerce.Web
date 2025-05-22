using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string,object> _repositories = [];
        public IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        {
            // Get Type name
            var typeName = typeof(TEntity).Name;

            // Dictionary<String,object>

            //if(_repositories.ContainsKey(typeName))
            //    return (IGenericRepository<TEntity,Tkey>) _repositories[key:typeName];
            // Refactor  for the previous two syntax lines
            if (_repositories.TryGetValue(typeName, out object? value))
                return (IGenericRepository<TEntity,Tkey>)value;
            else
            {
                // create object from repo
                var Repo = new GenericRepository<TEntity, Tkey>(_dbContext);
                // store object from repo in dictionary
                _repositories[key: typeName] = Repo;
                //return object
                return Repo;
            }
        }

        public async Task<int> SaveChangesAsync()=> await _dbContext.SaveChangesAsync();
        
    }
}
