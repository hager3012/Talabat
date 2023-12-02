using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.ProductSpecification
{
    public class productSpecWithFilterationAndCountPagination:BaseSpecification<product>
    {
        public productSpecWithFilterationAndCountPagination(productSpacificationParams spec) 
            : base
           (P =>
                  (string.IsNullOrEmpty(spec.Search) || (P.Name.ToLower().Contains(spec.Search))) &&
                  (!spec.BrandId.HasValue || P.BrandId == spec.BrandId.Value) &&
                  (!spec.CategoryId.HasValue || P.CategoryId == spec.CategoryId.Value))
        {
            
        }
    }
}
