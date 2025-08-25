using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using ServicesAbstraction;

namespace Services
{
    public class ServiceManager(IUnitOfWork _unitOfWork , IMapper _mapper ,IBasketRepository _basketRepository, UserManager<ApplicationUser> _userManager ) : IServiceManager
    {
        // Lazy Implementation

        //create lazy attribute to use it to initialize this property when is needed to be initialized
        private readonly Lazy<IProductService> _LazyProductService = new Lazy<IProductService>(valueFactory: () => new ProductService(_unitOfWork,_mapper));
        public IProductService ProductService => _LazyProductService.Value;

        private readonly Lazy<IBasketService> _LazyBasketService = new Lazy<IBasketService> (valueFactory: ()=> new BasketService(_basketRepository,_mapper));
        public IBasketService BasketService => _LazyBasketService.Value;

        private readonly Lazy<IAuthenticationService> _LazyAuthenticationService = new Lazy<IAuthenticationService>(valueFactory: () => new AuthenticationService(_userManager));
        public IAuthenticationService AuthenticationService => _LazyAuthenticationService.Value;
    }
}
