using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DomainLayer.Models.BasketModule;
using StackExchange.Redis;

namespace Persistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase _database= connection.GetDatabase();
        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? TimeToLive = null)
        {
            var JsonBasket = JsonSerializer.Serialize(value:basket);
            var IsCreatedOrUpdated= await _database.StringSetAsync(key: basket.Id, value: JsonBasket,expiry: TimeToLive ?? TimeSpan.FromDays(value:30));
            if (IsCreatedOrUpdated)
                return await GetBasketAsync(basket.Id);
            return null;

        }

        public async Task<bool> DeleteBasketAsync(string key) => await _database.KeyDeleteAsync(key);
        

        public async Task<CustomerBasket?> GetBasketAsync(string key)
        {
             var Basket = await _database.StringGetAsync(key);
            if(Basket.IsNullOrEmpty)
                return null;
            else
                return JsonSerializer.Deserialize<CustomerBasket>(json: Basket!);
        }
    }
}
