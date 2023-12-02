using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Repository.Contract;
using Talabat.Core.Servicies.Contract;
using Talabat.Core.Specifications.OrderSpcefication;

namespace Talabat.Servecies
{
    public class OrderService : IOrderServices
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IGenericRepository<product> _productRepo;
        //private readonly IGenericRepository<DeliveryMethod> _deliveryMethodRepo;
        //private readonly IGenericRepository<Order> _orderRepo;

        public OrderService(
            IBasketRepository basketRepo,
            //IGenericRepository<product> ProductRepo,
            //IGenericRepository<DeliveryMethod> DeliveryMethodRepo,
            //IGenericRepository<Order> OrderRepo
            IUnitOfWork unitOfWork
            )
        {
            _basketRepo = basketRepo;
            //this.unitOfWork = unitOfWork;
            //_productRepo = ProductRepo;
            //_deliveryMethodRepo = DeliveryMethodRepo;
            //_orderRepo = OrderRepo;
            _unitOfWork= unitOfWork;
        }
        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
        {
            // 1.Get basket from basket repo
            var basket = await _basketRepo.GetBasketById(basketId);
            // 2.Get Items at basket from product Repo
            var items = new List<OrderItem>();
            if(basket?.Items?.Count > 0)
            {
                var productRepo =  _unitOfWork.Repository<product>();
                foreach (var item in basket.Items)
                {
                    var product = await productRepo.GetAsnc(item.Id);
                    if(product != null)
                    {
                        var productItems = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);
                        var orderItem = new OrderItem(productItems,product.Price, item.Quantity);
                        items.Add(orderItem);
                    }
                    
                }
            }

            // 3.Calculate Subtotal 
            var subTotal = items.Sum(O => O.Price * O.Quantity);
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetAsnc(deliveryMethodId);
            // 4.Create Order
            var order = new Order(buyerEmail, shippingAddress, deliveryMethod, items,subTotal);
            await _unitOfWork.Repository<Order>().Add(order);
            // 5. Save To Database
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;
            return order;

        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            var deliveryMethod=_unitOfWork.Repository<DeliveryMethod>();
            var deliveryMethods=await deliveryMethod.GetAllAsnc();
            return deliveryMethods;
        }

        public async Task<Order?> GetOrderByIdForUserAsync(string buyerEmail, int Id)
        {
            var order = _unitOfWork.Repository<Order>();
            var orderSpec = new OrderSpcefications(buyerEmail,Id);
            var result = await order.GetWithSpecAsnc(orderSpec);
            if(result is null) return null;
            return result;

        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var orderRepo = _unitOfWork.Repository<Order>();
            var spec = new OrderSpcefications(buyerEmail);
            var orders = await orderRepo.GetAllWithSpecAsnc(spec);
            return orders;
        }
    }
}
