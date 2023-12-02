using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Core.Specifications.OrderSpcefication
{
    public class OrderSpcefications:BaseSpecification<Order>
    {
        public OrderSpcefications(string buyerEmail) 
            :base(O => O.BuyerEmail==buyerEmail)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
            AddOrderByDec(O => O.OrderDate);
        }
        public OrderSpcefications(string buyerEmail, int Id)
            :base(O => O.Id==Id && O.BuyerEmail==buyerEmail)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
        }
    }
}
