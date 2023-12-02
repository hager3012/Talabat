using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.ProductSpecification
{
    public class ProductWithBrandCategorySpecification :BaseSpecification<product>
    {
        public ProductWithBrandCategorySpecification(productSpacificationParams spacificationParams)
            :base
            (P =>
                 (string.IsNullOrEmpty(spacificationParams.Search)|| P.Name.ToLower().Contains(spacificationParams.Search))&&
                 (!spacificationParams.BrandId.HasValue || P.BrandId == spacificationParams.BrandId.Value) &&
                 (!spacificationParams.CategoryId.HasValue || P.CategoryId == spacificationParams.CategoryId.Value)
                 )
        {
            IncludesMethod();
            if (!string.IsNullOrEmpty(spacificationParams.Sort))
            {
                switch (spacificationParams.Sort)
                {
                    case "priceAsc":
                       AddOrderBy(P => P.Price); 
                        break;
                    case "priceDesc":
                        AddOrderByDec(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name); 
                        break;

                }
            }
            else
                AddOrderBy(P => P.Name);
            AddPagination((spacificationParams.PageIndex - 1) * spacificationParams.PageSize , spacificationParams.PageSize);
        }

        private void IncludesMethod()
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductCategory);
        }

        public ProductWithBrandCategorySpecification(int Id):base(P => P.Id==Id)
        {
            IncludesMethod();
        }
    }
}
