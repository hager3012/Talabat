
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core;
using Talabat.Core.Repository.Contract;
using Talabat.Core.Servicies.Contract;
using Talabat.Repository;
using Talabat.Servecies;

namespace Talabat.APIs.Extensions
{
    public static class AppService
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddScoped(typeof(IProductServices),typeof(ProductServices));
            services.AddScoped(typeof(IOrderServices), typeof(OrderService));
            services.AddScoped(typeof(IUnitOfWork),typeof(UnitOfWork));
            services.AddScoped(typeof(IBasketRepository), typeof(basketRepository));
            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(typeof(MappingProfile));
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (apiActionContext) =>
                {
                    var errors = apiActionContext.ModelState.Where(P => P.Value.Errors.Count > 0).
                    SelectMany(P => P.Value.Errors)
                    .Select(E => E.ErrorMessage);
                    var validationError = new ApiValidationResponse(400)
                    {
                        Errors = errors

                    };
                    return new BadRequestObjectResult(validationError);
                };


            });
            return services;
        }
    }
}
