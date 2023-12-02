using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specifications.ProductSpecification
{
    public class productSpacificationParams
    {
        private const int  MaxSize= 10;
        public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
        public int PageIndex { get; set; } = 1;
        public int pageSize=5;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > MaxSize ? MaxSize : value; }
        }
        private string? search;

        public string? Search
        {
            get { return search; }
            set { search = value?.ToLower(); }
        }

    }
}
