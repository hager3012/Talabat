using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Servicies.Contract;
using Talabat.Core.Specifications.ProductSpecification;

namespace Talabat.Servecies
{
    public class ProductServices : IProductServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IReadOnlyList<Brand>> GetBrandsAsnc()
             => await _unitOfWork.Repository<Brand>().GetAllAsnc();

        public async Task<IReadOnlyList<Category>> GetCategoriesAsync()
            => await _unitOfWork.Repository<Category>().GetAllAsnc();

        public async Task<int> GetCount(productSpacificationParams spacificationParams)
        {
            var productCountSpec = new productSpecWithFilterationAndCountPagination(spacificationParams);
            var Count =await _unitOfWork.Repository<product>().GetCountWithPagination(productCountSpec);
            return Count;
        }

        public async Task<product?> GetProductByIdAsync(int id)
        {
            var ProductSpec = new ProductWithBrandCategorySpecification(id);
            var product = await _unitOfWork.Repository<product>().GetWithSpecAsnc(ProductSpec);
            return product;
        }

        public async Task<IReadOnlyList<product>> GetProductsAsync(productSpacificationParams spacificationParams)
        {
            var ProductSpec = new ProductWithBrandCategorySpecification(spacificationParams);
            var Products =await _unitOfWork.Repository<product>().GetAllWithSpecAsnc(ProductSpec);
            return Products;
        }
    }
}
