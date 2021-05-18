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
    [Route("api/ProductReview")]
    [ApiController]
    public class ProductReviewController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProductReviewController(ApplicationDbContext context)
        {
            _context = context;
        }
        // <baseurl>/api/products
        [HttpGet("getAll")]
        public IActionResult GetProductReviews()
        {
            var productReviews = _context.ProductReviews;
            return Ok(productReviews);
        }

        [HttpGet("{id}/getUserReviews"), Authorize]
        public IActionResult UserReviews(string id)
        {
            try
            {
                var reviews = _context.ProductReviews.Where(u => u.UserId == id).Select(u => new { u.ReviewText, u.ReviewRating }).ToList();
                return Ok(reviews);

            }
            catch {
                return NotFound("User Not Found");
            }

        }

        [HttpPut("{id}/{productId}/review"), Authorize]
        public IActionResult EditReview(string id, int productId, [FromBody] ProductReview value)
        {
            try
            {
                var review = _context.ProductReviews.Where(u => (u.ProductId == productId && u.UserId == id)).SingleOrDefault();
                _context.ProductReviews.Remove(review);
                _context.ProductReviews.Add(value);
                _context.SaveChanges();
                return StatusCode(201, value);

            }
            catch
            {
                return NotFound();
            }

        }

        // POST /api/product
        [HttpPost("create"), Authorize]
        public IActionResult CreateProductReview([FromBody] ProductReview value)
        {
            /// Currently hard coded... Need to pass parameters
            _context.ProductReviews.Add(value);
            _context.SaveChanges();
            return StatusCode(201, value);
        }

        [HttpDelete("{productId}/{id}/deleteReview"), Authorize]
        public IActionResult DeleteProductReivew(string id, int productId)
        {
            var review = _context.ProductReviews.Where(u => (u.UserId == id && u.ProductId == productId)).SingleOrDefault();
            _context.Remove(review);
            _context.SaveChanges();
            return Ok();
        }
    }
}
