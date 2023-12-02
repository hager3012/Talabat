using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Repository.Contract
{
    public interface IBasketRepository
    {
        Task<customerBasket?> GetBasketById(string id);
        Task<bool> DeleteBasket(string id);
        Task<customerBasket?> UpdateBasket(customerBasket basket);
    }
}
