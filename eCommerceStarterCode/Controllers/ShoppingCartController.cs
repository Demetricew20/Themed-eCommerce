using eCommerceStarterCode.Data;
using eCommerceStarterCode.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;




namespace eCommerceStarterCode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet(), Authorize]
        public IActionResult GetCurrentUserCart()
        {
            var userId = User.FindFirstValue("id");
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var userCart = _context.ShoppingCarts.Where(uc => uc.UserId == userId)
                .Include(uc => uc.Product)
                .Select(uc => new { uc.UserId, uc.ProductId, uc.Product.ProductName, uc.Product.ProductDescription, uc.Quantity, uc.Product.ProductPrice, ExtPrice = uc.Quantity * uc.Product.ProductPrice})
                .ToList();
            return Ok(userCart);
        }

        [HttpGet("count"), Authorize]
        public IActionResult GetItemCount()
        {
            var userId = User.FindFirstValue("id");
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var userCart = _context.ShoppingCarts
                .GroupBy(uc => uc.UserId)
                .Select(uc => new { Count = uc.Count() });

            // returns {count: <int>}
            return Ok(userCart);

        }

        [HttpPut("{productId}"), Authorize]

        public IActionResult Put(int productId, [FromBody]ShoppingCart value)
        {
            var userId = User.FindFirstValue("id");
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var item = _context.ShoppingCarts.Where(u => (u.UserId == userId && u.ProductId == productId)).SingleOrDefault();
            _context.ShoppingCarts.Remove(item);
            _context.ShoppingCarts.Add(value);
            _context.SaveChanges();
            return Ok(value);

        }

        [HttpPost("{productId}"), Authorize]

        public IActionResult Post(int productId, [FromBody] ShoppingCart value)
        {
            var userId = User.FindFirstValue("id");
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var selectedObject = _context.ShoppingCarts.Where(sc => (sc.UserId == userId && sc.ProductId == productId)).SingleOrDefault();
            if (selectedObject != null)
            {
                selectedObject.Quantity = selectedObject.Quantity + value.Quantity;
                _context.ShoppingCarts.Update(selectedObject);
            } else {
                _context.ShoppingCarts.Add(value);
            }
                _context.SaveChanges();
                return StatusCode(201, selectedObject);
        }

        [HttpDelete("{productId}"), Authorize ]
        public IActionResult Delete(int productId)
        {
            var userId = User.FindFirstValue("id");
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var item = _context.ShoppingCarts.Where(u => (u.UserId == userId && u.ProductId == productId)).SingleOrDefault();
            _context.ShoppingCarts.Remove(item);
            _context.SaveChanges();
            return Ok();
        }




        
    }
}
