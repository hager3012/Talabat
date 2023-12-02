using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contract;
using Talabat.Core.Servicies.Contract;
using Talabat.Core.Specifications.ProductSpecification;

namespace Talabat.APIs.Controllers
{

    public class ProductController : BaseAPIController
    {
        //private readonly IGenericRepository<product> _productRepo;
        private readonly IMapper _mapper;
        private readonly IProductServices _productServices;

        //private readonly IGenericRepository<Category> _category;
        //private readonly IGenericRepository<Brand> _brand;

        public ProductController(IMapper mapper,IProductServices productServices)
        {
            //_productRepo = ProductRepo;
            _mapper = mapper;
            _productServices = productServices;
            //_category = Category;
            //_brand = Brand;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<product>>> GetProduct([FromQuery]productSpacificationParams spacificationParams)
        {
            
            var Products = await _productServices.GetProductsAsync(spacificationParams);
            var data = _mapper.Map<IReadOnlyList<product>, IReadOnlyList<ProductWithReturnDto>>(Products);
            var Count = await _productServices.GetCount(spacificationParams);
            return Ok(new ApplyPagination<ProductWithReturnDto>(spacificationParams.PageSize,spacificationParams.PageIndex,Count,data));
        }
        [HttpGet("{id}")]
         public async Task<ActionResult<product>> GetProductById(int id)
        {
           
            var product = await _productServices.GetProductByIdAsync(id);
            if(product is null)
                return NotFound();
            return Ok(_mapper.Map<product,ProductWithReturnDto>(product));
        }
        [HttpGet("Brand")]
        public async Task<ActionResult<IReadOnlyList<Brand>>> GetBrand()
        {
            var brand =await _productServices.GetBrandsAsnc();
            return Ok(brand);
        }
        [HttpGet("Category")]
        public async Task<ActionResult<IReadOnlyList<Category>>> GetCategory()
        {
            var category = await _productServices.GetCategoriesAsync();
            return Ok(category);
        }

    }

    internal class ApplyPagination
    {
        private int pageSize;
        private int pageIndex;
        private int count;

        public ApplyPagination(int pageSize, int pageIndex, int count)
        {
            this.pageSize = pageSize;
            this.pageIndex = pageIndex;
            this.count = count;
        }
    }
}
