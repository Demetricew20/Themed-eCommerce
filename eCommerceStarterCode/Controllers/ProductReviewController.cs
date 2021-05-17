using eCommerceStarterCode.Data;
using eCommerceStarterCode.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceStarterCode.Controllers
{
    [Route("api/product-review")]
    [ApiController]
    public class ProductReviewController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProductReviewController(ApplicationDbContext context)
        {
            _context = context;
        }
        // <baseurl>/api/products
        [HttpGet("get-all-reviews")]
        public IActionResult GetProductReviews()
        {
            var productReviews = _context.ProductReviews;
            return Ok(productReviews);
        }

        // POST /api/product
        [HttpPost("create"), Authorize]
        public IActionResult CreateProductReview([FromBody]ProductReview value)
        {
            /// Currently hard coded... Need to pass parameters
            _context.ProductReviews.Add(value);
            _context.SaveChanges();
            return StatusCode(201, value);
        }
    }
}
