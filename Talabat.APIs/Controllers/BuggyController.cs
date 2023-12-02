using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Repository;

namespace Talabat.APIs.Controllers
{
    public class BuggyController : BaseAPIController
    {
        private readonly StoreDbContext _dbContext;

        public BuggyController(StoreDbContext dbContext)
        {
           _dbContext = dbContext;
        }
        [HttpGet("notfound")]
        public ActionResult GetNotfound()
        {
            var product = _dbContext.Products.Find(100);
            if (product == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return NotFound();
        }
        [HttpGet("servererror")]
        public ActionResult GetServererror() 
        {
            var product = _dbContext.Products.Find(100);
            var ReturnProduct = product.ToString();
            return Ok(ReturnProduct);
        }
        [HttpGet("badrequest")]
        public ActionResult GetBadrequest() 
        {
            return BadRequest(new ApiResponse(400));
        }
        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadrequest(int Id)
        {
            return Ok();
        }
    }
}
