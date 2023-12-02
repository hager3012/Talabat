using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contract;

namespace Talabat.Repository
{
    public class basketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public basketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<bool> DeleteBasket(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }

        public async Task<customerBasket?> GetBasketById(string id)
        {
            var basket = await _database.StringGetAsync(id);
            return basket.IsNullOrEmpty?null: JsonSerializer.Deserialize<customerBasket>(basket);
        }

        public async Task<customerBasket?> UpdateBasket(customerBasket basket)
        {
            var basketCreatedOrUpdated = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));
            if (basketCreatedOrUpdated is false) return null;
            return await GetBasketById(basket.Id);
        }
    }
}
