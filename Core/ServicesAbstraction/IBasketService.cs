using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects.BasketModuleDtos;

namespace ServicesAbstraction
{
    public interface IBasketService
    {
        Task<BasketDto> GetBasketAsync(string key);
        Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basketDto);
        Task<bool> DeleteBasketAsync(string key);

    }
}
