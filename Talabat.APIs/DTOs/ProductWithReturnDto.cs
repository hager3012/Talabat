using AutoMapper;
using Talabat.Core.Entities;

namespace Talabat.APIs.DTOs
{
    public class ProductWithReturnDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int BrandId { get; set; }
        public string ProductBrandName { get; set; }
        public int CategoryId { get; set; }
        public string ProductCategoryName { get; set; }
    }
}
