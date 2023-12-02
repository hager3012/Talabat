using Talabat.Core.Entities;

namespace Talabat.APIs.DTOs
{
    public class customerBasketDto
    {
        public string Id { get; set; }
        public List<basketItemsDto> Items { get; set; }
        public customerBasketDto(string id)
        {
            Id = id;
            Items = new List<basketItemsDto>();
        }
    }
}
