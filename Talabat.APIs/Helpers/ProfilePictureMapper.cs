using AutoMapper;
using AutoMapper.Execution;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities;

namespace Talabat.APIs.Helpers
{
    public class ProfilePictureMapper : IValueResolver<product, ProductWithReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProfilePictureMapper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(product source, ProductWithReturnDto destination, string? destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{_configuration["BaseAPI"]}/{source.PictureUrl}";
            return string.Empty;
        }
    }
}
