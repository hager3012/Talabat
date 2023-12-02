using Talabat.Core.Entities;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.ProductSpecification;

namespace Talabat.APIs.Helpers
{
    public class ApplyPagination<T>
    {


        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Data { get; set; }
        public ApplyPagination(int pageSize, int pageIndex, int count, IReadOnlyList<T> data)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            Count = count;
            Data = data;
        }

    }

}
