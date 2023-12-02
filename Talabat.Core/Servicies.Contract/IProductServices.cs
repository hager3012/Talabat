using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications.ProductSpecification;

namespace Talabat.Core.Servicies.Contract
{
    public interface IProductServices
    {
        Task<IReadOnlyList<product>> GetProductsAsync(productSpacificationParams spacificationParams);
        Task<product?> GetProductByIdAsync(int id);
        Task<int> GetCount(productSpacificationParams spacificationParams);
        Task<IReadOnlyList<Brand>> GetBrandsAsnc();
        Task<IReadOnlyList<Category>> GetCategoriesAsync();
    }
}
