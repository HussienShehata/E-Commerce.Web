using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using ServicesAbstraction;

namespace Services
{
    public class ServiceManager(IUnitOfWork _unitOfWork , IMapper _mapper) : IServiceManager
    {
        // Lazy Implementation

        //create lazy attribute to use it to initialize this property when is needed to be initialized
        private readonly Lazy<IProductService> _LazyProductService = new Lazy<IProductService>(valueFactory: () => new ProductService(_unitOfWork,_mapper));
        public IProductService ProductService => _LazyProductService.Value;
    }
}
