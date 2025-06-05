using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.BasketModule;
using ServicesAbstraction;
using Shared.DataTransferObjects.BasketModuleDtos;

namespace Services
{
    public class BasketService(IBasketRepository _basketRepository , IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket)
        {
            var CustomerBasket = _mapper.Map<BasketDto, CustomerBasket>(basket);
            var CreatedOrUpdatedBasket = await _basketRepository.CreateOrUpdateBasketAsync(CustomerBasket);
            if (CreatedOrUpdatedBasket is not null)
                return await GetBasketAsync(basket.Id);
            else
                throw new Exception("Can not update or create the basket now , try again later");
        }

        public Task<bool> DeleteBasketAsync(string key) => _basketRepository.DeleteBasketAsync(key);
       

        public async Task<BasketDto> GetBasketAsync(string key)
        {
           var Basket =await _basketRepository.GetBasketAsync(key) ;
            if (Basket is not null)
                return  _mapper.Map<CustomerBasket, BasketDto>(Basket);
            else
                throw new BasketNotFoundException( key);
        }
    }
}

