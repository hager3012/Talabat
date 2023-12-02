using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class customerBasket
    {


        public string Id { get; set; }
        public List<basketItems> Items { get; set; }
        public customerBasket(string id)
        {
            Id = id;
            Items = new List<basketItems>();
        }
    }
}
