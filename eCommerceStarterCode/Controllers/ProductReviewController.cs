using eCommerceStarterCode.Data;
using eCommerceStarterCode.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

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
        [HttpGet()]
        public IActionResult GetAllReviews()
        {
            var productReviews = _context.ProductReviews;
            return Ok(productReviews);
        }

        [HttpGet("{productId}")]
        public IActionResult GetProductReview(int productId)
        {
            var productReview = _context.ProductReviews.Where(p => p.ProductId == productId).ToList();
            return Ok(productReview);
        }

        [HttpGet("/user"), Authorize]
        public IActionResult UserReviews()
        {
            var userId = User.FindFirstValue("id");
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            try
            {
                var reviews = _context.ProductReviews.Where(u => u.UserId == userId).Select(u => new { u.ReviewText, u.ReviewRating }).ToList();
                return Ok(reviews);

            }
            catch {
                return NotFound("No Reviews found");
            }

        }

        [HttpPut("{productId}/review"), Authorize]
        public IActionResult EditReview(int productId, [FromBody] ProductReview value)
        {
            var userId = User.FindFirstValue("id");
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }
            try
            {
                var review = _context.ProductReviews.Where(u => (u.ProductId == productId && u.UserId == userId)).SingleOrDefault();
                _context.ProductReviews.Remove(review);
                _context.ProductReviews.Add(value);
                _context.SaveChanges();
                return StatusCode(201, value);

            }
            catch
            {
                return NotFound("Review not found");
            }

        }

        // POST /api/ProductReview
        [HttpPost("rating/{id}/{productId}"), Authorize]
        public IActionResult AddRating(string id, int productId, [FromBody] ProductReview value)
        {
            try
            {   var user = _context.ProductReviews.Where(u => u.UserId == id).Select(u => u.UserId).SingleOrDefault();
                var product = _context.ProductReviews.Where(u => u.ProductId == productId).Select(u => u.ProductId).SingleOrDefault();
                var reviewText = _context.ProductReviews.Select(u => u.ReviewText).SingleOrDefault();
                var review = _context.ProductReviews.Where(u => (u.UserId == id && u.ProductId == productId)).SingleOrDefault();
                
                if (user == null || product == 0)
                {
                    ProductReview newReview = new ProductReview(){ 
                        UserId = id,
                        ProductId = productId,
                        ReviewRating = System.Convert.ToInt32(value.ReviewRating),
                        ReviewText = null,

                    };
                    _context.ProductReviews.Add(newReview);
                    _context.SaveChanges();
                    return StatusCode(201, newReview);
                }
                else
                {
                    ProductReview updateReview = new ProductReview() { 
                        UserId = id,
                        ProductId = productId,
                        ReviewRating = System.Convert.ToInt32(value.ReviewRating),
                        ReviewText = reviewText
                    };
                    _context.ProductReviews.Remove(review);
                    _context.ProductReviews.Add(updateReview);
                    _context.SaveChanges();
                    return StatusCode(201, updateReview);

                }
                 
                

            }
            catch
            {
                return NotFound();

            }
            
        }     
        // POST /api/product
        [HttpPost("review/{id}/{productId}"), Authorize]
        public IActionResult AddReview(string id, int productId, [FromBody] ProductReview value)
        {
            try
            {
                var user = _context.ProductReviews.Where(u => u.UserId == id).Select(u => u.UserId).SingleOrDefault();
                var product = _context.ProductReviews.Where(u => u.ProductId == productId).Select(u => u.ProductId).SingleOrDefault();
                var reviewRating = _context.ProductReviews.Where(u => (u.UserId == id && u.ProductId == productId)).Select(u=>u.ReviewRating).SingleOrDefault();
                var review = _context.ProductReviews.Where(u => (u.UserId == id && u.ProductId == productId)).SingleOrDefault();
                if (user == null | product == 0)
                {
                    ProductReview newReview = new ProductReview()
                    {
                        UserId = id,
                        ProductId = productId,
                        ReviewRating = 0,
                        ReviewText = value.ReviewText,

                    };
                    _context.ProductReviews.Add(newReview);
                    _context.SaveChanges();
                    return StatusCode(201, value);
                }
                else
                {
                    ProductReview updateReview = new ProductReview()
                    {
                        UserId = id,
                        ProductId = productId,
                        ReviewRating = System.Convert.ToInt32(reviewRating),
                        ReviewText = value.ReviewText
                    };
                    _context.ProductReviews.Remove(review);
                    _context.ProductReviews.Add(updateReview);
                    _context.SaveChanges();
                    return StatusCode(201, updateReview);

                }


            }
            catch
            {
                return NotFound();

            }
            
        }

        [HttpDelete("/{productId}"), Authorize]
        public IActionResult DeleteProductReivew(int productId)
        {
            var userId = User.FindFirstValue("id");
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            try
            {
                var review = _context.ProductReviews.Where(u => (u.UserId == userId && u.ProductId == productId)).SingleOrDefault();
                _context.Remove(review);
                _context.SaveChanges();
                return Ok();

            }
            catch
            {
                return NotFound("Could not delete record.");

            }
            
        }
    }
}
