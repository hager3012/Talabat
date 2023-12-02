using AutoMapper;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.APIs.Helpers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            _ = CreateMap<product, ProductWithReturnDto>()
                .ForMember(d => d.ProductBrandName, o => o.MapFrom(S => S.ProductBrand.Name))
                .ForMember(d => d.ProductCategoryName, o => o.MapFrom(S => S.ProductCategory))
                .ForMember(d => d.PictureUrl, O => O.MapFrom<ProfilePictureMapper>());
            CreateMap<customerBasketDto, customerBasket>();
            CreateMap<basketItemsDto, basketItems>();
            CreateMap<AddressDto, Address>();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethodName,O => O.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.DeliveryMethodCost,O => O.MapFrom(s => s.DeliveryMethod.Cost));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, O => O.MapFrom(s => s.Product.ProductId))
                .ForMember(d => d.ProductName, O => O.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.PictureUrl, O => O.MapFrom(s => s.Product.PictureUrl))
                .ForMember(d => d.PictureUrl , O => O.MapFrom<OrderPictureURL>());

        }
    }
}
